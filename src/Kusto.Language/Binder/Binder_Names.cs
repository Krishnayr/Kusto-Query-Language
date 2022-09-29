﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kusto.Language.Binding
{
    using Parsing;
    using Symbols;
    using Syntax;
    using Utils;

    internal sealed partial class Binder
    {
        private SemanticInfo BindName(string name, SymbolMatch match, SyntaxNode location, bool includeRowScope = true, bool inferColumns = true)
        {
            if (name == "")
                return ErrorInfo;

            var list = s_symbolListPool.AllocateFromPool();
            try
            {
                bool allowZeroArgumentInvocation = false;

                if (_pathScope != null)
                {
                    if (GetMacroExpandScope(location) is EntityGroupElementSymbol eges)
                    {
                        eges.GetMembers(name, match, list);
                        if (list.Count > 0)
                        {
                            return GetMatchingSymbolResult(name, location, list, allowZeroArgumentInvocation);
                        }
                    }

                    if (_pathScope == ScalarTypes.Dynamic)
                    {
                        // any x.y where x is dynamic, is also dynamic
                        return LiteralDynamicInfo;
                    }
                    else if (_pathScope == ScalarTypes.Unknown)
                    {
                        // any x.y where x is unknown, is also unknown (though probably dynamic)
                        return UnknownInfo;
                    }
                    else if (_pathScope == ErrorSymbol.Instance)
                    {
                        // any x.y where x is an error, is also an error
                        return ErrorInfo;
                    }
                    else if (_pathScope is GroupSymbol grp
                        && IsPassThrough(grp))
                    {
                        // get all symbols for all databases
                        var savePathScope = _pathScope;
                        foreach (var s in grp.Members)
                        {
                            _pathScope = s;
                            var alz = GetMatchingSymbols(name, match, location, list, includeRowScope, inferColumns);
                            allowZeroArgumentInvocation |= alz;
                        }
                        _pathScope = savePathScope;

                        MakeDistinct(list);
                        return GetMatchingSymbolResult(name, location, list, allowZeroArgumentInvocation);
                    }
                }
                else if (name == "$left" && _rowScope != null && _rightRowScope != null)
                {
                    var tuple = GetTuple(_rowScope);
                    return new SemanticInfo(tuple, tuple);
                }
                else if (name == "$right" && _rightRowScope != null)
                {
                    var tuple = GetTuple(_rightRowScope);
                    return new SemanticInfo(tuple, tuple);
                }

                if (list.Count == 0)
                {
                    allowZeroArgumentInvocation = GetMatchingSymbols(name, match, location, list, includeRowScope, inferColumns);
                }

                return GetMatchingSymbolResult(name, location, list, allowZeroArgumentInvocation);
            }
            finally
            {
                s_symbolListPool.ReturnToPool(list);
            }
        }

        private bool CanBindName(string name, SymbolMatch match, SyntaxNode location, bool includeRowScope = true, bool inferColumns = true)
        {
            if (name == "")
                return false;

            var list = s_symbolListPool.AllocateFromPool();
            try
            {
                if (_pathScope != null)
                {
                    if (_pathScope == ScalarTypes.Dynamic)
                    {
                        // any x.y where x is dynamic, is also dynamic
                        return true;
                    }
                    else if (_pathScope == ScalarTypes.Unknown)
                    {
                        // any x.y where x is unknown, is also unknown (though probably dynamic)
                        return true;
                    }
                    else if (_pathScope == ErrorSymbol.Instance)
                    {
                        // any x.y where x is an error, is also an error
                        return true;
                    }
                    else if (_pathScope is GroupSymbol grp
                        && IsPassThrough(grp))
                    {
                        // get all symbols for all databases
                        var savePathScope = _pathScope;
                        foreach (var s in grp.Members)
                        {
                            _pathScope = s;
                            var _ = GetMatchingSymbols(name, match, location, list, includeRowScope, inferColumns);
                        }
                        _pathScope = savePathScope;

                        return list.Count > 0;
                    }
                }
                else if (name == "$left" && _rowScope != null && _rightRowScope != null)
                {
                    return true;
                }
                else if (name == "$right" && _rightRowScope != null)
                {
                    return true;
                }

                if (list.Count == 0)
                {
                    var _ = GetMatchingSymbols(name, match, location, list, includeRowScope, inferColumns);
                }

                return list.Count > 0;
            }
            finally
            {
                s_symbolListPool.ReturnToPool(list);
            }
        }

        private static bool IsFunctionCallName(SyntaxNode name)
        {
            return name.Parent is FunctionCallExpression fn && fn.Name == name;
        }

        public bool IsInsideDatabaseFunctionDeclaration(SyntaxNode location)
        {
            // this is true when during expansion binding of database functions
            if (_currentFunction != null)
                return true;

            return IsInsideControlCommandFunctionDeclaration(location);
        }

        private static bool IsInsideControlCommandFunctionDeclaration(SyntaxNode location)
        {
            var functionDeclaration = location.GetFirstAncestor<FunctionDeclaration>();
            if (functionDeclaration != null && functionDeclaration.Parent is CustomNode)
                return true;

            var functionBody = location.GetFirstAncestor<FunctionBody>();
            if (functionBody != null && functionBody.Parent is CustomNode)
                return true;

            return false;
        }

        private static bool IsInsideControlCommand(SyntaxNode location)
        {
            // so far, only control commands use CustomNode
            return location.GetFirstAncestor<CustomNode>() != null;
        }

        private static bool IsInsideControlCommandProper(SyntaxNode location)
        {
            // its part of the control command but not part of any
            // function declaration or input query
            return IsInsideControlCommand(location)
                && !IsInsideControlCommandFunctionDeclaration(location)
                && !IsInsideControlCommandInputQuery(location);
        }

        private static bool IsInsideControlCommandInputQuery(SyntaxNode location)
        {
            // looking for statement list that is child of a CustomNode
            return location.GetFirstAncestor<SyntaxList<SeparatedElement<Statement>>>(
                s => s.Parent is CustomNode) != null;
        }

        private static bool IsInvocableFunctionName(SyntaxNode location)
        {
            // function names in non-executable parts of control commands are not invocable
            return !IsInsideControlCommandProper(location);
        }

        private static bool IsPossibleInvocableFunctionWithoutArgumentList(SyntaxNode location)
        {
            return !IsFunctionCallName(location)
                && IsInvocableFunctionName(location);
        }

        private static bool IsEvaluateFunctionName(SyntaxNode name)
        {
            return name.Parent is FunctionCallExpression fn
                && fn.Parent is EvaluateOperator;
        }

        /// <summary>
        /// True if member access operators (dot) on this apply to the members
        /// as opposed to matching the members themselves.
        /// </summary>
        private static bool IsPassThrough(GroupSymbol group)
        {
            // dot access on groups of clusters or databases is meant to be 
            // the aggregate of the dot access on all the clusters or databases
            return group.Members.Any(m => m is DatabaseSymbol || m is ClusterSymbol);
        }

        private static void MakeDistinct(List<Symbol> list)
        {
            if (list.Count > 1)
            {
                var hset = s_symbolHashSetPool.AllocateFromPool();
                var newList = s_symbolListPool.AllocateFromPool();

                foreach (var item in list)
                {
                    if (!hset.Contains(item))
                    {
                        hset.Add(item);
                        newList.Add(item);
                    }
                }

                list.Clear();
                list.AddRange(newList);

                s_symbolListPool.ReturnToPool(newList);
                s_symbolHashSetPool.ReturnToPool(hset);
            }
        }

        private bool GetMatchingSymbols(string name, SymbolMatch match, SyntaxNode location, List<Symbol> list, bool includeRowScope, bool inferColumns)
        {
            var allowZeroArgumentInvocation = false;

            if (IsFunctionCallName(location))
            {
                if (_pathScope is DatabaseSymbol)
                {
                    if (name == Functions.Table.Name)
                    {
                        list.Add(Functions.Table);
                    }
                    else if (name == Functions.ExternalTable.Name)
                    {
                        list.Add(Functions.ExternalTable);
                    }
                    else if (name == Functions.MaterializedView.Name)
                    {
                        list.Add(Functions.MaterializedView);
                    }
                    else
                    {
                        _pathScope.GetMembers(name, SymbolMatch.Function, list);
                    }
                }
                else if (_pathScope is ClusterSymbol && name == Functions.Database.Name)
                {
                    list.Add(Functions.Database);
                }
                else
                {
                    GetFunctionsInScope(_scopeKind, name, IncludeFunctionKind.All, list);
                }
            }
            else
            {
                // don't match the database functions that have same name as database tables
                // if we are inside declaration of a database function
                if (IsInsideDatabaseFunctionDeclaration(location) &&
                    _currentDatabase.GetAnyTable(name) != null)
                {
                    match &= ~SymbolMatch.Function;
                }

                // if there is a path scope, then the operation was <path>.<name>
                if (_pathScope != null)
                {
                    // check for inferred columns associated with scan operator step variables (encoded as tuples associated with a table)
                    if (_pathScope is TupleSymbol tuple
                        && tuple.RelatedTable != null
                        && tuple.RelatedTable.IsOpen
                        && inferColumns
                        && TryGetDeclaredOrInferredColumn(tuple.RelatedTable, name, out var col))
                    {
                        list.Add(col);
                    }
                    // database('...').name
                    else if (_pathScope is DatabaseSymbol ds)
                    {
                        // first look for functions
                        _pathScope.GetMembers(name, match & SymbolMatch.Function, list);
                        RemoveFunctionsThatCannotBeInvokedWithZeroArgs(list);

                        // database functions don't require argument lists to invoke
                        allowZeroArgumentInvocation = list.Count > 0;

                        if (list.Count == 0)
                        {
                            // next look for anything else (tables)
                            _pathScope.GetMembers(name, match & ~SymbolMatch.Function, list);
                        }

                        // otherwise this is possible an open table
                        if (list.Count == 0 && ds.IsOpen)
                        {
                            var table = GetOpenTable(name, ds);
                            list.Add(table);
                            return allowZeroArgumentInvocation;
                        }
                    }
                    // kusto does not allow Table.Column, unless its part of a control command
                    else if (!(_pathScope is TableSymbol) || IsInsideControlCommandProper(location))
                    {
                        // lookup named members
                        _pathScope.GetMembers(name, match, list);
                    }
                }

                // check binding against any columns in the row scope
                // note: the row scope is the scope containing the columns from the left that are in scope on the right of a pipe operator.
                if (list.Count == 0 && _rowScope != null && includeRowScope)
                {
                    _rowScope.GetMembers(name, match, list);
                }

                // try secondary right-side row scope (used in join operator)
                if (list.Count == 0 && _rightRowScope != null)
                {
                    _rightRowScope.GetMembers(name, match, list);
                }

                // try local variables (includes any user-defined functions)
                // these are defined by a previous let statement
                if (list.Count == 0)
                {
                    _localScope.GetSymbols(name, match, list);

                    // user defined functions do not require argument list if it has no arguments
                    allowZeroArgumentInvocation = list.Count > 0;
                }

                // look for zero-argument database functions
                if (list.Count == 0 && IsPossibleInvocableFunctionWithoutArgumentList(location)
                    && (match & SymbolMatch.Function) != 0)
                {
                    // database functions only (locally defined functions are already handled above)
                    GetFunctionsInScope(_scopeKind, name, IncludeFunctionKind.DatabaseFunctions, list);
                    RemoveFunctionsThatCannotBeInvokedWithZeroArgs(list);

                    // database functions do not require argument list if it has zero arguments.
                    allowZeroArgumentInvocation = list.Count > 0;
                }

                // other items in database (tables, etc)
                if (list.Count == 0 && _currentDatabase != null)
                {
                    _currentDatabase.GetMembers(name, match, list);
                }

                // databases can be directly referenced in commands
                if (list.Count == 0 && _currentCluster != null && (match & SymbolMatch.Database) != 0)
                {
                    _currentCluster.GetMembers(name, match, list);
                }

                // look for any built-in functions with matching name (even with those with parameters)
                if (list.Count == 0 && (match & SymbolMatch.Function) != 0)
                {
                    GetFunctionsInScope(_scopeKind, name, IncludeFunctionKind.BuiltInFunctions, list);
                }

                // infer column for this otherwise unbound reference?
                if (list.Count == 0 && _rowScope != null && _rowScope.IsOpen && (match & SymbolMatch.Column) != 0
                    && includeRowScope && inferColumns)
                {
                    // row scope table has open definition, so create an inferred column for the otherwise unbound name
                    list.Add(GetOpenTableInferredColumn(name, _rowScope));
                }
            }

            return allowZeroArgumentInvocation;
        }

        private SemanticInfo GetMatchingSymbolResult(string name, SyntaxNode location, List<Symbol> matches, bool allowZeroArgumentInvocation)
        {
            if (matches.Count == 1)
            {
                var item = matches[0];
                var resultType = GetResultType(item);

                // check for zero-parameter function invocation not part of a function call node
                if (resultType is FunctionSymbol fn && IsPossibleInvocableFunctionWithoutArgumentList(location))
                {
                    var sig = fn.Signatures.FirstOrDefault(s => s.MinArgumentCount == 0);
                    if (sig != null && allowZeroArgumentInvocation)
                    {
                        var funResult = GetFunctionCallResult(sig, EmptyReadOnlyList<Expression>.Instance, EmptyReadOnlyList<TypeSymbol>.Instance);
                        return new SemanticInfo(item, funResult.Type, calledFunctionInfo: funResult.Info);
                    }
                    else
                    {
                        var returnType = GetCommonReturnType(fn.Signatures, EmptyReadOnlyList<Expression>.Instance, EmptyReadOnlyList<TypeSymbol>.Instance);
                        return new SemanticInfo(item, returnType, DiagnosticFacts.GetFunctionRequiresArgumentList(name).WithLocation(location));
                    }
                }
                else if (resultType is EntityGroupSymbol eg)
                {
                    // entity group symbols are like function symbols that have a body to be evaluated
                    // in order to determine their result type
                    var result = GetFunctionCallResult(eg.Signature, EmptyReadOnlyList<Expression>.Instance, EmptyReadOnlyList<TypeSymbol>.Instance);
                    return new SemanticInfo(item, result.Type, calledFunctionInfo: result.Info);
                }
                else
                {
                    return CreateSemanticInfo(item);
                }
            }
            else if (matches.Count == 0)
            {
                if (IsFunctionCallName(location))
                {
                    if (_globals.GetAggregate(name) != null && _scopeKind != ScopeKind.Aggregate)
                    {
                        return new SemanticInfo(
                            ErrorSymbol.Instance,
                            DiagnosticFacts.GetAggregateNotAllowedInThisContext(name).WithLocation(location));
                    }
                    else if (_globals.GetPlugIn(name) != null && _scopeKind != ScopeKind.PlugIn)
                    {
                        return new SemanticInfo(
                            ErrorSymbol.Instance,
                            DiagnosticFacts.GetPluginNotAllowedInThisContext(name).WithLocation(location));
                    }
                    else if (IsEvaluateFunctionName(location))
                    {
                        if (PlugIns.GetPlugIn(name) != null)
                        {
                            return new SemanticInfo(
                                ErrorSymbol.Instance,
                                DiagnosticFacts.GetPlugInFunctionIsNotEnabled(name).WithLocation(location));
                        }
                        else
                        {
                            return new SemanticInfo(
                                ErrorSymbol.Instance,
                                DiagnosticFacts.GetPlugInFunctionNotDefined(name).WithLocation(location));
                        }
                    }
                    else if (IsFuzzyUnionOperand(location))
                    {
                        return null;
                    }
                    else if (_pathScope is DatabaseSymbol ds)
                    {
                        if (ds != null && ds.IsOpen)
                        {
                            return new SemanticInfo(new TableSymbol("").WithIsOpen(true));
                        }
                        else
                        {
                            return new SemanticInfo(
                                new TableSymbol("").WithIsOpen(true),
                                DiagnosticFacts.GetNameDoesNotReferToAnyKnownFunction(name).WithLocation(location));
                        }
                    }
                    else if (IsInTabularContext(location))
                    {
                        return new SemanticInfo(
                            new TableSymbol("").WithIsOpen(true),
                            DiagnosticFacts.GetNameDoesNotReferToAnyKnownFunction(name).WithLocation(location));
                    }
                    else if (IsPossibleInternalFunction(name))
                    {
                        // if it looks like it might be an internal function, give it a pass.
                        return UnknownInfo;
                    }
                    else
                    {
                        return new SemanticInfo(
                            ErrorSymbol.Instance,
                            DiagnosticFacts.GetNameDoesNotReferToAnyKnownFunction(name).WithLocation(location));
                    }
                }
                else if (IsInTabularContext(location))
                {
                    if (IsFuzzyUnionOperand(location))
                    {
                        return new SemanticInfo(
                            new TableSymbol().WithIsOpen(true),
                            DiagnosticFacts.GetFuzzyUnionOperandNotDefined(name).WithLocation(location));
                    }
                    else if (_pathScope is DatabaseSymbol ds
                        && ds.IsOpen)
                    {
                        return new SemanticInfo(new TableSymbol(name).WithIsOpen(true));
                    }
                    else
                    {
                        return new SemanticInfo(
                            new TableSymbol(name).WithIsOpen(true),
                            DiagnosticFacts.GetNameDoesNotReferToAnyKnownTable(name).WithLocation(location));
                    }
                }
                else
                {
                    return new SemanticInfo(
                        ErrorSymbol.Instance,
                        DiagnosticFacts.GetNameDoesNotReferToAnyKnownItem(name).WithLocation(location));
                }
            }
            else
            {
                return new SemanticInfo(
                    new GroupSymbol(matches.ToList()),
                    ErrorSymbol.Instance,
                    DiagnosticFacts.GetNameRefersToMoreThanOneItem(name).WithLocation(location));
            }
        }

        private static bool IsPossibleInternalFunction(string name)
        {
            return name.StartsWith("__");
        }

        private static void RemoveFunctionsThatCannotBeInvokedWithZeroArgs(List<Symbol> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] is FunctionSymbol fn && fn.MinArgumentCount > 0)
                {
                    list.RemoveAt(i);
                }
            }
        }

        private static void GetWildcardSymbols(string pattern, IReadOnlyList<Symbol> symbols, List<Symbol> matchingSymbols)
        {
            foreach (var symbol in symbols)
            {
                if (KustoFacts.Matches(pattern, symbol.Name))
                {
                    matchingSymbols.Add(symbol);
                }
            }
        }

        /// <summary>
        /// Determine if the element (a name) is in a known tabular context
        /// </summary>
        private static bool IsInTabularContext(SyntaxElement element)
        {
            // if function call name, look further up to determine context
            if (element.Parent is FunctionCallExpression fc
                && fc.Name == element)
            {
                element = element.Parent;
            }

            // if inside parenthesis or part of a list, look further up
            while (element.Parent is ParenthesizedExpression
                || element.Parent is SyntaxList
                || element.Parent is SeparatedElement)
            {
                element = element.Parent;
            }

            if (element.Parent != null)
            {
                // if x | op then x is expected to be tabular
                if (element.Parent is PipeExpression pe && pe.Expression == element)
                {
                    return true;
                }

                // if database().x then x is expected to be tabular
                if (element.Parent is PathExpression pt
                    && pt.Selector == element)
                {
                    if (pt.Expression.ResultType is DatabaseSymbol)
                        return true;

                    if (pt.Expression.ResultType is GroupSymbol g
                        && g.Members.Any(m => m is DatabaseSymbol))
                        return true;
                }

                // use completion hint to help us determine if context is tabular
                var hint = element.Parent.GetCompletionHint(element.IndexInParent);

                if (hint == Editor.CompletionHint.Table
                    || hint == Editor.CompletionHint.Tabular
                    || hint == Editor.CompletionHint.MaterializedView
                    || hint == Editor.CompletionHint.ExternalTable)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determine if the element is the operand of a fuzzy union
        /// </summary>
        private static bool IsFuzzyUnionOperand(SyntaxElement element)
        {
            if (element.Parent is FunctionCallExpression fc && fc.Name == element)
            {
                element = element.Parent;
            }

            while (element.Parent is ParenthesizedExpression
                || element.Parent is SyntaxList
                || element.Parent is SeparatedElement)
            {
                element = element.Parent;
            }

            var context = element.Parent;

            if (context != null)
            {
                if (context is PipeExpression pe && pe.Expression == element)
                {
                    context = pe.Operator;
                }

                if (context is UnionOperator uo)
                {
                    return uo.Parameters.GetParameterLiteralValue<bool>(QueryOperatorParameters.IsFuzzy);
                }
            }

            return false;
        }
    }
}