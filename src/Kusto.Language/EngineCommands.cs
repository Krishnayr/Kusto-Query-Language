﻿using System.Collections.Generic;

namespace Kusto.Language
{
    using Symbols;

    public static class EngineCommands
    {
        private static readonly string UnknownResult = "()";

        #region Schema Commands
        #region Databases
        private static readonly string ShowDatabaseResults =
            "(DatabaseName: string, PersistentStorage: string, Version: string, IsCurrent: bool, DatabaseAccessMode: string, PrettyName: string, ReservedSlot1: bool, DatabaseId: guid, InTransitionTo: string)";

        private static readonly string ShowDatabaseDetailsResults =
            "(DatabaseName: string, PersistentStorage: string, Version: string, IsCurrent: bool, DatabaseAccessMode: string, PrettyName: string, AuthorizedPrincipals: string, RetentionPolicy: string, MergePolicy: string, ReservedSlot1: string, CachingPolicy: string, ShardingPolicy: string, StreamingIngestionPolicy: string, IngestionBatchingPolicy: string, TotalSize: real, DatabaseId: guid, InTransitionTo: string)";

        private static readonly string DatabasesNameList = "'(' { DatabaseName=<database>, ',' }+ ')'";

        public static readonly CommandSymbol ShowDatabase =
            new CommandSymbol(nameof(ShowDatabase),
                "show database",
                ShowDatabaseResults);

        public static readonly CommandSymbol ShowDatabaseDetails =
            new CommandSymbol(nameof(ShowDatabaseDetails),
                "show database details",
                ShowDatabaseDetailsResults);

        public static readonly CommandSymbol ShowDatabaseIdentity =
            new CommandSymbol(nameof(ShowDatabaseIdentity),
                "show database identity",
                "(DatabaseName: string, PersistentStorage: string, Version: string, IsCurrent: bool, DatabaseAccessMode: string, PrettyName: string, CurrentUserIsUnrestrictedViewer: bool, DatabaseId: guid, InTransitionTo: string)");

        public static readonly CommandSymbol ShowDatabasePolicies =
            new CommandSymbol(nameof(ShowDatabasePolicies),
                "show database policies",
                "(DatabaseName: string, PersistentStorage: string, Version: string, IsCurrent: bool, DatabaseAccessMode: string, PrettyName: string, DatabaseId: guid, AuthorizedPrincipals: string, RetentionPolicy: string, MergePolicy: string, CachingPolicy: string, ShardingPolicy: string, StreamingIngestionPolicy: string, IngestionBatchingPolicy: string)");

        public static readonly CommandSymbol ShowDatabaseDataStats =
            new CommandSymbol(nameof(ShowDatabaseDataStats),
                "show database datastats",
                "(DatabaseName: string, PersistentStorage: string, Version: string, IsCurrent: bool, DatabaseAccessMode: string, PrettyName: string, " +
                    "CurrentUseIsUnrestrictedViewer: bool, DatabaseId: guid, " +
                    "OriginalSize: real, ExtentSize: real, CompressedSize: real, IndexSize: real, RowCount: long, " +
                    "HotOriginalSize: real, HotExtentSize: real, HotCompressedSize: real, HotIndexSize: real, HotRowCount: long)");

        public static readonly CommandSymbol ShowClusterDatabases =
            new CommandSymbol(nameof(ShowClusterDatabases),
                "show [cluster] databases ['(' { DatabaseName=<database>, ',' }+ ')']",
                "(DatabaseName: string, PersistentStorage: string, Version: string, IsCurrent: bool, DatabaseAccessMode: string, PrettyName: string, ReservedSlot1: bool, DatabaseId: guid, InTransitionTo: string)");

        public static readonly CommandSymbol ShowClusterDatabasesDetails =
            new CommandSymbol(nameof(ShowClusterDatabasesDetails),
                "show [cluster] databases details",
                "(DatabaseName: string, PersistentStorage: string, Version: string, IsCurrent: bool, DatabaseAccessMode: string, PrettyName: string, AuthorizedPrincipals: string, RetentionPolicy: string, MergePolicy: string, ReservedSlot1: string, CachingPolicy: string, ShardingPolicy: string, StreamingIngestionPolicy: string, IngestionBatchingPolicy: string, TotalSize: real, DatabaseId: guid, InTransitionTo: string)");

        public static readonly CommandSymbol ShowClusterDatabasesIdentity =
            new CommandSymbol(nameof(ShowClusterDatabasesIdentity),
                "show [cluster] databases identity",
                "(DatabaseName: string, PersistentStorage: string, Version: string, IsCurrent: bool, DatabaseAccessMode: string, PrettyName: string, CurrentUserIsUnrestrictedViewer: bool, DatabaseId: guid, InTransitionTo: string)");

        public static readonly CommandSymbol ShowClusterDatabasesPolicies =
            new CommandSymbol(nameof(ShowClusterDatabasesPolicies),
                "show [cluster] databases policies",
                "(DatabaseName: string, PersistentStorage: string, Version: string, IsCurrent: bool, DatabaseAccessMode: string, PrettyName: string, DatabaseId: guid, AuthorizedPrincipals: string, RetentionPolicy: string, MergePolicy: string, CachingPolicy: string, ShardingPolicy: string, StreamingIngestionPolicy: string, IngestionBatchingPolicy: string)");

        public static readonly CommandSymbol ShowClusterDatabasesDataStats =
            new CommandSymbol(nameof(ShowClusterDatabasesDataStats),
                "show [cluster] databases datastats",
                "(DatabaseName: string, PersistentStorage: string, Version: string, IsCurrent: bool, DatabaseAccessMode: string, PrettyName: string, DatabaseId: guid, OriginalSize: real, ExtentSize: real, CompressedSize: real, IndexSize: real, RowCount: long, HotOriginalSize: real, HotExtentSize: real, HotCompressedSize: real, HotIndexSize: real, HotRowCount: long)");

        public static readonly CommandSymbol CreateDatabasePersist =
            new CommandSymbol(nameof(CreateDatabasePersist),
                "create database DatabaseName=<name> persist '(' { Container=<string>, ',' }+ ')' [ifnotexists]",
                "(DatabaseName: string, PersistentPath: string, Created: string, StoresMetadata: bool, StoresData: bool)");

        public static readonly CommandSymbol CreateDatabaseVolatile =
            new CommandSymbol(nameof(CreateDatabaseVolatile),
                "create database DatabaseName=<name> volatile [ifnotexists]",
                "(DatabaseName: string, PersistentPath: string, Created: bool, StoresMetadata: bool, StoresData: bool)");

        public static readonly CommandSymbol AttachDatabase =
            new CommandSymbol(nameof(AttachDatabase),
                "attach database DatabaseName=<database> from (BlobContainerUrl=<string> ';' StorageAccountKey=<string> | Path=<string>)",
                "(Step: string, Duration: string)");

        public static readonly CommandSymbol AttachDatabaseMetadata =
            new CommandSymbol(nameof(AttachDatabaseMetadata),
                "attach database metadata DatabaseName=<database> from (BlobContainerUrl=<string> ';' StorageAccountKey=<string> | Path=<string>)",
                "(Step: string, Duration: string)");

        public static readonly CommandSymbol DetachDatabase =
            new CommandSymbol(nameof(DetachDatabase),
                "detach database DatabaseName=<database>",
                "(Table: string, NumberOfRemovedExtents: string)");

        public static readonly CommandSymbol AlterDatabasePrettyName =
            new CommandSymbol(nameof(AlterDatabasePrettyName),
                "alter database DatabaseName=<database> prettyname DatabasePrettyName=<string>",
                "(DatabaseName: string, PrettyName: string)");

        public static readonly CommandSymbol DropDatabasePrettyName =
            new CommandSymbol(nameof(DropDatabasePrettyName),
                "drop database DatabaseName=<database> prettyname",
                "(DatabaseName: string, PrettyName: string)");

        public static readonly CommandSymbol AlterDatabasePersistMetadata =
            new CommandSymbol(nameof(AlterDatabasePersistMetadata),
                "alter database DatabaseName=<database> persist metadata (BlobContainerUrl=<string> ';' StorageAccountKey=<string> | Path=<string>)",
                "(Moniker: guid, Url: string, State: string, CreatedOn: datetime, MaxDateTime: datetime, IsRecyclable: bool, StoresDatabaseMetadata: bool, HardDeletePeriod: timespan)");

        public static readonly CommandSymbol SetAccess =
            new CommandSymbol(nameof(SetAccess),
                "set access DatabaseName=<database> to AccessMode=(readonly | readwrite)",
                "(DatabaseName: string, RequestedAccessMode: string, Status: string)");

        public static readonly CommandSymbol ShowDatabaseSchema =
            new CommandSymbol(nameof(ShowDatabaseSchema),
                "show database [databaseName=<database>] schema [details] [if_later_than databaseVersion=<string>]",
                "(DatabaseName: string, TableName: string, ColumnName: string, ColumnType: string, IsDefaultTable: bool, IsDefaultColumn: bool, PrettyName: string, Version: string, Folder: string, DocString: string)");

        public static readonly CommandSymbol ShowDatabaseSchemaAsJson =
            new CommandSymbol(nameof(ShowDatabaseSchemaAsJson),
                "show database (schema | DatabaseName=<database> schema) [if_later_than Version=<string>] as json",
                "(DatabaseSchema: string)");

        public static readonly CommandSymbol ShowDatabaseSchemaAsCslScript =
            new CommandSymbol(nameof(ShowDatabaseSchemaAsCslScript),
                $"show database (schema | DatabaseName=<database> schema) [if_later_than Version=<string>] as csl script [{PropertyList()}]",
                "(DatabaseSchemaScript: string)");

        public static readonly CommandSymbol ShowDatabaseCslSchema =
            new CommandSymbol(nameof(ShowDatabaseCslSchema),
                $"show database [databaseName=<database>] cslschema [script] [if_later_than databaseVersion=<string>]",
                "(TableName: string, Schema: string, DatabaseName: string, Folder: string, DocString: string)");

        public static readonly CommandSymbol ShowDatabaseSchemaViolations =
            new CommandSymbol(nameof(ShowDatabaseSchemaViolations),
                "show database databaseName=<database> schema violations",
                "(EntityKind: string, EntityName: string, Property: string, Reason: string)");

        public static readonly CommandSymbol ShowDatabasesSchema =
            new CommandSymbol(nameof(ShowDatabasesSchema),
                "show databases '(' { DatabaseName=<database> [if_later_than Version=<string>], ',' }+ ')' schema [details]",
                "(DatabaseName: string, TableName: string, ColumnName: string, ColumnType: string, IsDefaultTable: bool, IsDefaultColumn: bool, PrettyName: string, Version: string, Folder: string, DocString: string)");

        public static readonly CommandSymbol ShowDatabasesSchemaAsJson =
            new CommandSymbol(nameof(ShowDatabasesSchemaAsJson),
                "show databases '(' { DatabaseName=<database> [if_later_than Version=<string>], ',' }+ ')' schema as json",
                "(DatabaseSchema: string)");

        private readonly static string DatabaseIngestionMappingResult =
           "(Name: string, Kind: string, Mapping: string, LastUpdatedOn: datetime, Database: string)";

        public static readonly CommandSymbol CreateDatabaseIngestionMapping =
            new CommandSymbol(nameof(CreateDatabaseIngestionMapping),
                "create database DatabaseName=<name> ingestion! MappingKind=(csv | json | avro | parquet | orc | w3clogfile) mapping MappingName=<string> MappingFormat=<string>",
                DatabaseIngestionMappingResult);

        public static readonly CommandSymbol AlterDatabaseIngestionMapping =
            new CommandSymbol(nameof(AlterDatabaseIngestionMapping),
                "alter database DatabaseName=<database> ingestion MappingKind=(csv | json | avro | parquet | orc | w3clogfile) mapping MappingName=<string> MappingFormat=<string>",
                DatabaseIngestionMappingResult);

        public static readonly CommandSymbol ShowDatabaseIngestionMappings =
            new CommandSymbol(nameof(ShowDatabaseIngestionMappings),
                $"show database [databaseName=<database>] ingestion [kind=(csv | avro | apacheavro | json | parquet | sstream | orc | w3clogfile)] mappings [name=<string>] [{PropertyList()}]",
                DatabaseIngestionMappingResult);

        public static readonly CommandSymbol ShowIngestionMappings =
            new CommandSymbol(nameof(ShowIngestionMappings),
                $"show [cluster] ingestion [kind=(csv | avro | apacheavro | json | parquet | sstream | orc | w3clogfile)] mappings [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol DropDatabaseIngestionMapping =
            new CommandSymbol(nameof(DropDatabaseIngestionMapping),
                "drop database DatabaseName=<database> ingestion MappingKind=(csv | json | avro | parquet | orc | w3clogfile) mapping MappingName=<string>",
                DatabaseIngestionMappingResult);


        #endregion

        #region Tables
        private static readonly string ExtentIdList = "'(' {ExtentId=<guid>, ','}+ ')'";

        private static readonly string ShowTablesResult =
            "(TableName: string, DatabaseName: string, Folder: string, DocString: string)";

        private static readonly string ShowTablesDetailsResult =
            "(TableName: string, DatabaseName: string, Folder: string, DocString: string, TotalExtents: long, TotalExtentSize: real, TotalOriginalSize: real, TotalRowCount: long, HotExtents: long, HotExtentSize: real, HotOriginalSize: real, HotRowCount: long, AuthorizedPrincipals: string, RetentionPolicy: string, CachingPolicy: string, ShardingPolicy: string, MergePolicy: string, StreamingIngestionPolicy: string, IngestionBatchingPolicy: string, MinExtentsCreationTime: datetime, MaxExtentsCreationTime: datetime, RowOrderPolicy: string, TableId: guid)";

        private static readonly string ShowTableSchemaResult =
            "(TableName: string, Schema: string, DatabaseName: string, Folder: string, DocString: string)";

        public static readonly CommandSymbol ShowTables =
            new CommandSymbol(nameof(ShowTables),
                "show tables ['(' { TableName=<table>, ',' }+ ')']",
                ShowTablesResult);

        public static readonly CommandSymbol ShowTable =
            new CommandSymbol(nameof(ShowTable),
                "show table TableName=<table>",
                "(AttributeName: string, AttributeType: string, ExtentSize: long, CompressionRatio: real, IndexSize: long, IndexSizePercent: real, OriginalSize: long, AttributeId: guid, SharedIndexSize: long, StorageEngineVersion: string)");

        public static readonly CommandSymbol ShowTablesDetails =
            new CommandSymbol(nameof(ShowTablesDetails),
                "show tables ['(' { TableName=<table>, ',' }+ ')'] details",
                ShowTablesDetailsResult);

        public static readonly CommandSymbol ShowTableDetails =
            new CommandSymbol(nameof(ShowTableDetails),
                "show table TableName=<table> details",
                ShowTablesDetailsResult);

        public static readonly CommandSymbol ShowTableCslSchema =
            new CommandSymbol(nameof(ShowTableCslSchema),
                "show table TableName=<table> cslschema",
                ShowTableSchemaResult);

        public static readonly CommandSymbol ShowTableSchemaAsJson =
            new CommandSymbol(nameof(ShowTableSchemaAsJson),
                "show table TableName=<table> schema as json",
                ShowTableSchemaResult);

        private static readonly string TableSchema = "('(' { ColumnName=<name> ':'! ColumnType=<type>, ',' }+ ')')";

        private static readonly string TableProperties = PropertyList("docstring | folder");

        public static readonly CommandSymbol CreateTable =
            new CommandSymbol(nameof(CreateTable),
                $"create table TableName=<name> {TableSchema} [{TableProperties}]",
                ShowTableSchemaResult);

        public static readonly CommandSymbol CreateTableBasedOnAnother =
            new CommandSymbol(nameof(CreateTableBasedOnAnother),
                $"create table NewTableName=<name> based-on TableName=<name> [{TableProperties}]",
                ShowTableSchemaResult);

        public static readonly CommandSymbol CreateMergeTable =
            new CommandSymbol(nameof(CreateMergeTable),
                $"create-merge table TableName=<name> {TableSchema}",
                ShowTableSchemaResult);

        public static readonly CommandSymbol CreateTables =
            new CommandSymbol(nameof(CreateTables),
                $"create tables {{ TableName=<name> {TableSchema}, ',' }}+ [{PropertyList()}]",
                "(TableName: string, DatabaseName: string, Folder: string, DocString: string)");

        public static readonly CommandSymbol CreateMergeTables =
            new CommandSymbol(nameof(CreateMergeTables),
                $"create-merge tables {{ TableName=<name> {TableSchema}, ',' }}+ [{PropertyList()}]",
                "(TableName: string, DatabaseName: string, Folder: string, DocString: string)");

        public static readonly CommandSymbol DefineTables =
            new CommandSymbol(nameof(DefineTables),
                $"define tables {{ TableName=<name> {TableSchema}, ',' }}+ [{PropertyList()}]",
                "(TableName: string, DatabaseName: string, Folder: string, DocString: string)");

        public static readonly CommandSymbol AlterTable =
            new CommandSymbol(nameof(AlterTable),
                $"alter table <table> {TableSchema} [{TableProperties}]",
                ShowTableSchemaResult);

        public static readonly CommandSymbol AlterMergeTable =
            new CommandSymbol(nameof(AlterMergeTable),
                $"alter-merge table <table> {TableSchema} [{TableProperties}]",
                ShowTableSchemaResult);

        public static readonly CommandSymbol AlterTableDocString =
            new CommandSymbol(nameof(AlterTableDocString),
                "alter table TableName=<table> docstring Documentation=<string>",
                "(TableName: string, Schema: string, DatabaseName: string, Folder: string, DocString: string)");

        public static readonly CommandSymbol AlterTableFolder =
            new CommandSymbol(nameof(AlterTableFolder),
                "alter table TableName=<table> folder Folder=<string>",
                "(TableName: string, Schema: string, DatabaseName: string, Folder: string, DocString: string)");

        public static readonly CommandSymbol RenameTable =
            new CommandSymbol(nameof(RenameTable),
                "rename table TableName=<table> to NewTableName=<name>",
                ShowTablesResult);

        public static readonly CommandSymbol RenameTables =
            new CommandSymbol(nameof(RenameTables),
                "rename tables { NewTableName=<name> '='! TableName=<table>, ',' }+",
                ShowTablesResult);

        public static readonly CommandSymbol DropTable =
            new CommandSymbol(nameof(DropTable),
                "drop table TableName=<table> [ifexists]",
                "(TableName: string, DatabaseName: string)");

        public static readonly CommandSymbol UndoDropTable =
            new CommandSymbol(nameof(UndoDropTable),
                "undo drop table <name> [as TableName=<name>] version '=' Version=<string>",
                "(ExtentId: guid, NumberOfRecords: long, Status: string, FailureReason: string)");

        private static readonly string TableNameList = "'(' { TableName=<table>, ',' }+ ')'";

        public static readonly CommandSymbol DropTables =
            new CommandSymbol(nameof(DropTables),
                $"drop tables {TableNameList} [ifexists]",
                "(TableName: string, DatabaseName: string)");

        private readonly static string TableIngestionMappingResult =
            "(Name: string, Kind: string, Mapping: string, LastUpdatedOn: datetime, Database: string, Table: string)";

        public static readonly CommandSymbol CreateTableIngestionMapping =
            new CommandSymbol(nameof(CreateTableIngestionMapping),
                "create table TableName=<name> ingestion! MappingKind=(csv | json | avro | parquet | orc | w3clogfile) mapping MappingName=<string> MappingFormat=<string>",
                TableIngestionMappingResult);

        public static readonly CommandSymbol AlterTableIngestionMapping =
            new CommandSymbol(nameof(AlterTableIngestionMapping),
                "alter table TableName=<table> ingestion MappingKind=(csv | json | avro | parquet | orc | w3clogfile) mapping MappingName=<string> MappingFormat=<string>",
                TableIngestionMappingResult);

        public static readonly CommandSymbol ShowTableIngestionMappings =
            new CommandSymbol(nameof(ShowTableIngestionMappings),
                "show table TableName=<table> ingestion MappingKind=(csv | json | avro | parquet | orc | w3clogfile) mappings",
                TableIngestionMappingResult);

        public static readonly CommandSymbol ShowTableIngestionMapping =
            new CommandSymbol(nameof(ShowTableIngestionMapping),
                "show table TableName=<table> ingestion MappingKind=(csv | json | avro | parquet | orc | w3clogfile) mapping MappingName=<string>",
                TableIngestionMappingResult);

        public static readonly CommandSymbol DropTableIngestionMapping =
            new CommandSymbol(nameof(DropTableIngestionMapping),
                "drop table TableName=<table> ingestion MappingKind=(csv | json | avro | parquet | orc | w3clogfile) mapping MappingName=<string>",
                TableIngestionMappingResult);
        #endregion

        #region Columns

        public static readonly CommandSymbol RenameColumn =
            new CommandSymbol(nameof(RenameColumn),
                "rename column ColumnName=<database_table_column> to NewColumnName=<name>",
                "(EntityName: string, DataType: string, Policy: string)");

        public static readonly CommandSymbol RenameColumns =
            new CommandSymbol(nameof(RenameColumns),
                "rename columns { NewColumnName=<name> '='! ColumnName=<database_table_column>, ',' }+",
                "(EntityName: string, DataType: string, Policy: string)");

        public static readonly CommandSymbol AlterColumnType =
            new CommandSymbol(nameof(AlterColumnType),
                "alter column ColumnName=<database_table_column> type '=' ColumnType=<type>",
                "(EntityName: string, DataType: string, Policy: string)");

        public static readonly CommandSymbol DropColumn =
            new CommandSymbol(nameof(DropColumn),
                "drop column ColumnName=<table_column>",
                "(TableName: string, Schema: string, DatabaseName: string, Folder: string, DocString: string)");

        public static readonly CommandSymbol DropTableColumns =
            new CommandSymbol(nameof(DropTableColumns),
                "drop table TableName=<table> columns '(' { ColumnName=<column>, ',' }+ ')'",
                "(TableName: string, Schema: string, DatabaseName: string, Folder: string, DocString: string)");

        public static readonly CommandSymbol AlterTableColumnDocStrings =
            new CommandSymbol(nameof(AlterTableColumnDocStrings),
                "alter table TableName=<table> column-docstrings '(' { ColumnName=<column> ':'! DocString=<string>, ',' }+ ')'",
                "(TableName: string, Schema: string, DatabaseName: string, Folder: string, DocString: string)");

        public static readonly CommandSymbol AlterMergeTableColumnDocStrings =
            new CommandSymbol(nameof(AlterMergeTableColumnDocStrings),
                "alter-merge table TableName=<table> column-docstrings '(' { ColumnName=<column> ':'! DocString=<string>, ',' }+ ')'",
                "(TableName: string, Schema: string, DatabaseName: string, Folder: string, DocString: string)");
        #endregion

        #region Functions
        private static readonly string FunctionResult =
            "(Name: string, Parameters: string, Body: string, Folder: string, DocString: string)";

        public static readonly CommandSymbol ShowFunctions =
            new CommandSymbol(nameof(ShowFunctions),
                "show functions",
                FunctionResult);

        public static readonly CommandSymbol ShowFunction =
            new CommandSymbol(nameof(ShowFunction),
                "show function FunctionName=<function>",
                FunctionResult);

        public static readonly CommandSymbol CreateFunction =
            new CommandSymbol(nameof(CreateFunction),
                "create function [ifnotexists] [with '('! { PropertyName=<name> '='! Value=<value>, ',' } ')'!] FunctionName=<name> <function_declaration>",
                FunctionResult);

        public static readonly CommandSymbol AlterFunction =
            new CommandSymbol(nameof(AlterFunction),
                "alter function [with '('! { PropertyName=<name> '='! Value=<value>, ',' }+ ')'!] FunctionName=<function> <function_declaration>",
                FunctionResult);

        public static readonly CommandSymbol CreateOrAlterFunction =
            new CommandSymbol(nameof(CreateOrAlterFunction),
                "create-or-alter function [with '('! { PropertyName=<name> '='! Value=<value>, ',' }+ ')'!] FunctionName=<name> <function_declaration>",
                FunctionResult);

        private static readonly string FunctionNameList = "'(' { FunctionName=<function>, ',' }+ ')'";

        public static readonly CommandSymbol DropFunction =
            new CommandSymbol(nameof(DropFunction),
                "drop function FunctionName=<function> [ifexists]",
                "(Name: string)");

        public static readonly CommandSymbol DropFunctions =
            new CommandSymbol(nameof(DropFunctions),
                $"drop functions {FunctionNameList} [ifexists]",
                FunctionResult);

        public static readonly CommandSymbol AlterFunctionDocString =
            new CommandSymbol(nameof(AlterFunctionDocString),
                "alter function <function> docstring Documentation=<string>",
                FunctionResult);

        public static readonly CommandSymbol AlterFunctionFolder =
            new CommandSymbol(nameof(AlterFunctionFolder),
                "alter function FunctionName=<function> folder Folder=<string>",
                FunctionResult);
        #endregion

        #region External Tables
        private static readonly string ExternalTableResult =
            "(TableName: string, TableType: string, Folder: string, DocString: string, Properties: string, ConnectionStrings: dynamic, Partitions: dynamic, PathFormat: string)";

        private static readonly string ExternalTableSchemaResult =
            "(TableName: string, Schema: string, DatabaseName: string, Folder: string, DocString: string)";

        private static readonly string ExternalTableArtifactsResult =
            "(Uri: string, Partition: dynamic, Size: long)";

        private static readonly string ExternalTableFullResult =
            "(TableName: string, TableType: string, Folder: string, DocString: string, Schema: string, Properties: string)";

        public static readonly CommandSymbol ShowExternalTables =
            new CommandSymbol(nameof(ShowExternalTables),
                "show external tables",
                ExternalTableResult);

        public static readonly CommandSymbol ShowExternalTable =
            new CommandSymbol(nameof(ShowExternalTable),
                "show external table ExternalTableName=<externaltable>",
                ExternalTableResult);

        public static readonly CommandSymbol ShowExternalTableCslSchema =
            new CommandSymbol(nameof(ShowExternalTableCslSchema),
                "show external table ExternalTableName=<externaltable> cslschema",
                ExternalTableSchemaResult);

        public static readonly CommandSymbol ShowExternalTableSchema =
            new CommandSymbol(nameof(ShowExternalTableSchema),
                "show external table ExternalTableName=<externaltable> schema as (json | csl)",
                ExternalTableSchemaResult);

        public static readonly CommandSymbol ShowExternalTableArtifacts =
            new CommandSymbol(nameof(ShowExternalTableArtifacts),
                "show external table ExternalTableName=<externaltable> artifacts [limit LimitCount=<long>]",
                ExternalTableArtifactsResult);

        public static readonly CommandSymbol DropExternalTable =
            new CommandSymbol(nameof(DropExternalTable),
                "drop external table ExternalTableName=<externaltable>",
                ExternalTableResult);

        private static readonly string CreateOrAlterStorageExternalTableGrammar =
            @"external table ExternalTableName=<name> '(' { ColumnName=<name> ':'! ColumnType=<type>, ',' }+ ')'
              kind '='! TableKind=(storage | #blob | #adl)
              [partition by!
               '('
                {PartitionName=<name> ':'!
                 (PartitionType=string ['=' StringColumn=<name>]
                  | PartitionType=datetime ['='
                    (PartitionFunction=bin '('! DateTimeColumn=<name> ',' BinValue=<timespan> ')'
                     | PartitionFunction=(startofday | startofweek | startofmonth | startofyear) '('! DateTimeColumn=<name> ')')]
                  | PartitionType=long '='! PartitionFunction=hash '(' StringColumn=<name> ',' HashMod=<long> ')'), ','}+
               ')'
               [pathformat '='! '('
                [PathSeparator=<string>]
                { (PartitionName=<name> | datetime_pattern '('! DateTimeFormat=<string> ',' PartitionName=<name> ')')
                 [PathSeparator=<string>] }+ ')']
              ]
              dataformat '='! DataFormatKind=(avro | apacheavro | csv | json | multijson | orc | parquet | psv | raw | scsv | sohsv | sstream | tsv | tsve | txt | w3clogfile)
              '(' { StorageConnectionString=<string>, ',' }+ ')'
              [with '('! { PropertyName=<name> '='! Value=<value>, ',' }+ ')']";

        private static readonly string CreateOrAlterSqlExternalTableGrammar =
            @"external table ExternalTableName=<name> '(' { ColumnName=<name> ':'! ColumnType=<type>, ',' }+ ')'
              kind '='! TableKind=(sql)
              table '=' <name>
              '(' <string> ')'
              [with '('! { PropertyName=<name> '='! Value=<value>, ',' }+ ')']";

        public static readonly CommandSymbol CreateStorageExternalTable =
            new CommandSymbol(nameof(CreateStorageExternalTable),
                "create " + CreateOrAlterStorageExternalTableGrammar,
                ExternalTableResult);

        public static readonly CommandSymbol AlterStorageExternalTable =
            new CommandSymbol(nameof(AlterStorageExternalTable),
                "alter " + CreateOrAlterStorageExternalTableGrammar,
                ExternalTableFullResult);

        public static readonly CommandSymbol CreateOrAlterStorageExternalTable =
            new CommandSymbol(nameof(CreateOrAlterStorageExternalTable),
                "create-or-alter " + CreateOrAlterStorageExternalTableGrammar,
                ExternalTableFullResult);

        public static readonly CommandSymbol CreateSqlExternalTable =
            new CommandSymbol(nameof(CreateSqlExternalTable),
                "create " + CreateOrAlterSqlExternalTableGrammar,
                ExternalTableResult);

        public static readonly CommandSymbol AlterSqlExternalTable =
            new CommandSymbol(nameof(AlterSqlExternalTable),
                "alter " + CreateOrAlterSqlExternalTableGrammar,
                ExternalTableFullResult);

        public static readonly CommandSymbol CreateOrAlterSqlExternalTable =
            new CommandSymbol(nameof(CreateOrAlterSqlExternalTable),
                "create-or-alter " + CreateOrAlterSqlExternalTableGrammar,
                ExternalTableFullResult);

        public static readonly CommandSymbol CreateExternalTableMapping =
            new CommandSymbol(nameof(CreateExternalTableMapping),
                "create external table ExternalTableName=<externaltable> mapping MappingName=<string> MappingFormat=<string>",
                TableIngestionMappingResult);

        public static readonly CommandSymbol SetExternalTableAdmins =
            new CommandSymbol(nameof(SetExternalTableAdmins),
                "set external table externalTableName=<externaltable> admins (none [skip-results] | '(' {principal=<string>, ','}+ ')' [skip-results] [notes=<string>])",
                UnknownResult);

        public static readonly CommandSymbol AddExternalTableAdmins =
            new CommandSymbol(nameof(AddExternalTableAdmins),
                "add external table externalTableName=<externaltable> admins '(' {principal=<string>, ','}+ ')' [skip-results] [notes=<string>]",
                UnknownResult);

        public static readonly CommandSymbol DropExternalTableAdmins =
            new CommandSymbol(nameof(DropExternalTableAdmins),
                "drop external table externalTableName=<externaltable> admins '(' {principal=<string>, ','}+ ')' [skip-results] [notes=<string>]",
                UnknownResult);

        public static readonly CommandSymbol AlterExternalTableDocString =
            new CommandSymbol(nameof(AlterExternalTableDocString),
                "alter external table tableName=<externaltable> docstring docStringValue=<string>",
                UnknownResult);

        public static readonly CommandSymbol AlterExternalTableFolder =
            new CommandSymbol(nameof(AlterExternalTableFolder),
                "alter external table tableName=<externaltable> folder folderValue=<string>",
                UnknownResult);

        public static readonly CommandSymbol ShowExternalTablePrincipals =
            new CommandSymbol(nameof(ShowExternalTablePrincipals),
                "show external table tableName=<externaltable> principals",
                UnknownResult);

        public static readonly CommandSymbol ShowFabric =
            new CommandSymbol(nameof(ShowFabric),
                "show fabric id=<name>",
                UnknownResult);




        public static readonly CommandSymbol AlterExternalTableMapping =
            new CommandSymbol(nameof(AlterExternalTableMapping),
                "alter external table ExternalTableName=<externaltable> mapping MappingName=<string> MappingFormat=<string>",
                TableIngestionMappingResult);

        public static readonly CommandSymbol ShowExternalTableMappings =
            new CommandSymbol(nameof(ShowExternalTableMappings),
                "show external table ExternalTableName=<externaltable> mappings",
                TableIngestionMappingResult);

        public static readonly CommandSymbol ShowExternalTableMapping =
            new CommandSymbol(nameof(ShowExternalTableMapping),
                "show external table ExternalTableName=<externaltable> mapping MappingName=<string>",
                TableIngestionMappingResult);

        public static readonly CommandSymbol DropExternalTableMapping =
            new CommandSymbol(nameof(DropExternalTableMapping),
                "drop external table ExternalTableName=<externaltable> mapping MappingName=<string>",
                TableIngestionMappingResult);
        #endregion

        #region Workload groups
        private static readonly string WorkloadGroupResult =
            "(WorkloadGroupName: string, WorkloadGroup:string)";

        public static readonly CommandSymbol ShowWorkloadGroups =
            new CommandSymbol(nameof(ShowWorkloadGroups),
                "show workload_groups",
                WorkloadGroupResult);

        public static readonly CommandSymbol ShowWorkloadGroup =
            new CommandSymbol(nameof(ShowWorkloadGroup),
                "show workload_group WorkloadGroup=<name>",
                WorkloadGroupResult);

        public static readonly CommandSymbol CreateOrAleterWorkloadGroup =
            new CommandSymbol(nameof(CreateOrAleterWorkloadGroup),
                "create-or-alter workload_group WorkloadGroupName=<name> WorkloadGroup=<string>",
                WorkloadGroupResult);

        public static readonly CommandSymbol AlterMergeWorkloadGroup =
            new CommandSymbol(nameof(AlterMergeWorkloadGroup),
                "alter-merge workload_group WorkloadGroupName=<name> WorkloadGroup=<string>",
                WorkloadGroupResult);

        public static readonly CommandSymbol DropWorkloadGroup =
            new CommandSymbol(nameof(DropWorkloadGroup),
                "drop workload_group WorkloadGroupName=<name>",
                WorkloadGroupResult);
        #endregion
        #endregion

        #region Policy Commands
        private static readonly string PolicyResult =
            "(PolicyName: string, EntityName: string, Policy: string, ChildEntities: string, EntityType: string)";

        #region Caching
        public static readonly CommandSymbol ShowDatabasePolicyCaching =
            new CommandSymbol(nameof(ShowDatabasePolicyCaching),
                "show database DatabaseName=(<database> | '*') policy caching",
                PolicyResult);

        public static readonly CommandSymbol ShowTablePolicyCaching =
            new CommandSymbol(nameof(ShowTablePolicyCaching),
                "show table TableName=<database_table> policy caching",
                PolicyResult);

        public static readonly CommandSymbol ShowTableStarPolicyCaching =
            new CommandSymbol(nameof(ShowTableStarPolicyCaching),
                "show table '*' policy caching",
                PolicyResult);

        public static readonly CommandSymbol ShowColumnPolicyCaching =
            new CommandSymbol(nameof(ShowColumnPolicyCaching),
                "show column ColumnName=(<database_table_column> | '*') policy caching",
                PolicyResult);

        public static readonly CommandSymbol ShowMaterializedViewPolicyCaching =
            new CommandSymbol(nameof(ShowMaterializedViewPolicyCaching),
                "show materialized-view MaterializedViewName=<materializedview> policy caching",
                PolicyResult);

        public static readonly CommandSymbol ShowClusterPolicyCaching =
            new CommandSymbol(nameof(ShowClusterPolicyCaching),
                "show cluster policy caching",
                PolicyResult);

        private static readonly string HotPolicy =
            "(hot '='! Timespan=<timespan> | hotdata '='! HotData=<timespan> hotindex '='! HotIndex=<timespan>)";

        public static readonly CommandSymbol AlterDatabasePolicyCaching =
            new CommandSymbol(nameof(AlterDatabasePolicyCaching),
                $"alter database DatabaseName=<database> policy caching {HotPolicy}",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicyCaching =
            new CommandSymbol(nameof(AlterTablePolicyCaching),
                $"alter table TableName=<database_table> policy caching {HotPolicy}",
                PolicyResult);

        public static readonly CommandSymbol AlterTablesPolicyCaching =
            new CommandSymbol(nameof(AlterTablesPolicyCaching),
                $"alter tables '(' {{TableName=<table>, ','}}+ ')' policy caching {HotPolicy} [[','] {{hot_window '=' p=(d1=<datetime> '..' d2=<datetime>), ','}}+]",
                UnknownResult);

        public static readonly CommandSymbol AlterColumnPolicyCaching =
            new CommandSymbol(nameof(AlterColumnPolicyCaching),
                $"alter column ColumnName=<database_table_column> policy caching {HotPolicy}",
                PolicyResult);

        public static readonly CommandSymbol AlterMaterializedViewPolicyCaching =
            new CommandSymbol(nameof(AlterMaterializedViewPolicyCaching),
                $"alter materialized-view MaterializedViewName=<materializedview> policy caching {HotPolicy}",
                PolicyResult);

        public static readonly CommandSymbol AlterClusterPolicyCaching =
            new CommandSymbol(nameof(AlterClusterPolicyCaching),
                $"alter cluster policy caching {HotPolicy}",
                PolicyResult);

        public static readonly CommandSymbol DeleteDatabasePolicyCaching =
            new CommandSymbol(nameof(DeleteDatabasePolicyCaching),
                "delete database DatabaseName=<database> policy caching",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicyCaching =
            new CommandSymbol(nameof(DeleteTablePolicyCaching),
                "delete table TableName=<database_table> policy caching",
                PolicyResult);

        public static readonly CommandSymbol DeleteColumnPolicyCaching =
            new CommandSymbol(nameof(DeleteColumnPolicyCaching),
                "delete column ColumnName=<database_table_column> policy caching",
                PolicyResult);

        public static readonly CommandSymbol DeleteMaterializedViewPolicyCaching =
            new CommandSymbol(nameof(DeleteMaterializedViewPolicyCaching),
                "delete materialized-view MaterializedViewName=<materializedview> policy caching",
                PolicyResult);

        public static readonly CommandSymbol DeleteClusterPolicyCaching =
            new CommandSymbol(nameof(DeleteClusterPolicyCaching),
                "delete cluster policy caching",
                PolicyResult);
        #endregion

        #region IngestionTime
        public static readonly CommandSymbol ShowTablePolicyIngestionTime =
            new CommandSymbol(nameof(ShowTablePolicyIngestionTime),
                "show table TableName=<table> policy ingestiontime",
                PolicyResult);

        public static readonly CommandSymbol ShowTableStarPolicyIngestionTime =
            new CommandSymbol(nameof(ShowTableStarPolicyIngestionTime),
                "show table '*' policy ingestiontime",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicyIngestionTime =
            new CommandSymbol(nameof(AlterTablePolicyIngestionTime),
                "alter table TableName=<table> policy ingestiontime true",
                PolicyResult);

        public static readonly CommandSymbol AlterTablesPolicyIngestionTime =
            new CommandSymbol(nameof(AlterTablesPolicyIngestionTime),
                "alter tables '(' { TableName=<table>, ',' }+ ')' policy ingestiontime true",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicyIngestionTime =
            new CommandSymbol(nameof(DeleteTablePolicyIngestionTime),
                "delete table TableName=<table> policy ingestiontime",
                PolicyResult);
        #endregion

        #region Retention
        public static readonly CommandSymbol ShowTablePolicyRetention =
            new CommandSymbol(nameof(ShowTablePolicyRetention),
                "show table TableName=<database_table> policy retention",
                PolicyResult);

        public static readonly CommandSymbol ShowTableStarPolicyRetention =
            new CommandSymbol(nameof(ShowTableStarPolicyRetention),
                "show table '*' policy retention",
                PolicyResult);

        public static readonly CommandSymbol ShowDatabasePolicyRetention =
            new CommandSymbol(nameof(ShowDatabasePolicyRetention),
                "show database DatabaseName=(<database> | '*') policy retention",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicyRetention =
            new CommandSymbol(nameof(AlterTablePolicyRetention),
                "alter table TableName=<database_table> policy retention RetentionPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMaterializedViewPolicyRetention =
            new CommandSymbol(nameof(AlterMaterializedViewPolicyRetention),
                "alter materialized-view MaterializedViewName=<materializedview> policy retention RetentionPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterDatabasePolicyRetention =
            new CommandSymbol(nameof(AlterDatabasePolicyRetention),
                "alter database DatabaseName=<database> policy retention RetentionPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterTablesPolicyRetention =
            new CommandSymbol(nameof(AlterTablesPolicyRetention),
                "alter tables '(' { TableName=<table>, ',' }+ ')' policy retention RetentionPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeTablePolicyRetention =
            new CommandSymbol(nameof(AlterMergeTablePolicyRetention),
                "alter-merge table TableName=<database_table> policy retention (RetentionPolicy=<string> | softdelete '='! SoftDeleteValue=<timespan> [recoverability '='! RecoverabilityValue=(disabled|enabled)] | recoverability '='! RecoverabilityValue=(disabled|enabled))",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeMaterializedViewPolicyRetention =
            new CommandSymbol(nameof(AlterMergeMaterializedViewPolicyRetention),
                "alter-merge materialized-view MaterializedViewName=<materializedview> policy retention (RetentionPolicy=<string> | softdelete '='! SoftDeleteValue=<timespan> [recoverability '='! RecoverabilityValue=(disabled|enabled)] | recoverability '='! RecoverabilityValue=(disabled|enabled))",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeDatabasePolicyRetention =
            new CommandSymbol(nameof(AlterMergeDatabasePolicyRetention),
                "alter-merge database DatabaseName=<database> policy retention (RetentionPolicy=<string> | softdelete '='! SoftDeleteValue=<timespan> [recoverability '='! RecoverabilityValue=(disabled|enabled)] | recoverability '='! RecoverabilityValue=(disabled|enabled))",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicyRetention =
            new CommandSymbol(nameof(DeleteTablePolicyRetention),
                "delete table TableName=<database_table> policy retention",
                PolicyResult);

        public static readonly CommandSymbol DeleteDatabasePolicyRetention =
            new CommandSymbol(nameof(DeleteDatabasePolicyRetention),
                "delete database DatabaseName=<database> policy retention",
                PolicyResult);

        public static readonly CommandSymbol ShowDatabasePolicyHardRetentionViolations =
            new CommandSymbol(nameof(ShowDatabasePolicyHardRetentionViolations),
                "show database databaseName=<database> policy hardretention violations",
                UnknownResult);

        public static readonly CommandSymbol ShowDatabasePolicySoftRetentionViolations =
            new CommandSymbol(nameof(ShowDatabasePolicySoftRetentionViolations),
                "show database databaseName=<database> policy softretention violations",
                UnknownResult);

        #endregion

        #region RowLevelSecurity
        public static readonly CommandSymbol ShowTablePolicyRowLevelSecurity =
            new CommandSymbol(nameof(ShowTablePolicyRowLevelSecurity),
                "show table TableName=<table> policy row_level_security",
                PolicyResult);

        public static readonly CommandSymbol ShowTableStarPolicyRowLevelSecurity =
            new CommandSymbol(nameof(ShowTableStarPolicyRowLevelSecurity),
                "show table '*' policy row_level_security",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicyRowLevelSecurity =
            new CommandSymbol(nameof(AlterTablePolicyRowLevelSecurity),
                "alter table TableName=<table> policy row_level_security (enable | disable) [with '('! { PropertyName=<name> '='! Value=<value>, ',' } ')'!] Query=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicyRowLevelSecurity =
            new CommandSymbol(nameof(DeleteTablePolicyRowLevelSecurity),
                "delete table TableName=<table> policy row_level_security",
                PolicyResult);

        public static readonly CommandSymbol ShowMaterializedViewPolicyRowLevelSecurity =
            new CommandSymbol(nameof(ShowMaterializedViewPolicyRowLevelSecurity),
                "show materialized-view MaterializedViewName=<materializedview> policy row_level_security",
                PolicyResult);

        public static readonly CommandSymbol AlterMaterializedViewPolicyRowLevelSecurity =
            new CommandSymbol(nameof(AlterMaterializedViewPolicyRowLevelSecurity),
                "alter materialized-view MaterializedViewName=<materializedview> policy row_level_security (enable | disable) Query=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteMaterializedViewPolicyRowLevelSecurity =
            new CommandSymbol(nameof(DeleteMaterializedViewPolicyRowLevelSecurity),
                "delete materialized-view MaterializedViewName=<materializedview> policy row_level_security",
                PolicyResult);
        #endregion

        #region RowOrder
        public static readonly CommandSymbol ShowTablePolicyRowOrder =
            new CommandSymbol(nameof(ShowTablePolicyRowOrder),
                "show table TableName=<database_table> policy roworder",
                PolicyResult);

        public static readonly CommandSymbol ShowTableStarPolicyRowOrder =
            new CommandSymbol(nameof(ShowTableStarPolicyRowOrder),
                "show table '*' policy roworder",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicyRowOrder =
            new CommandSymbol(nameof(AlterTablePolicyRowOrder),
                "alter table TableName=<database_table> policy roworder '('! { ColumnName=<column> (asc|desc)!, ',' }+ ')'!",
                PolicyResult);

        public static readonly CommandSymbol AlterTablesPolicyRowOrder =
            new CommandSymbol(nameof(AlterTablesPolicyRowOrder),
                "alter tables '(' { TableName=<table>, ',' }+ ')' policy roworder '(' { ColumnName=<name> (asc|desc)!, ',' }+ ')'",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeTablePolicyRowOrder =
            new CommandSymbol(nameof(AlterMergeTablePolicyRowOrder),
                "alter-merge table TableName=<database_table> policy roworder '(' { ColumnName=<column> (asc|desc)!, ',' }+ ')'",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicyRowOrder =
            new CommandSymbol(nameof(DeleteTablePolicyRowOrder),
                "delete table TableName=<database_table> policy roworder",
                PolicyResult);
        #endregion

        #region Update
        public static readonly CommandSymbol ShowTablePolicyUpdate =
            new CommandSymbol(nameof(ShowTablePolicyUpdate),
                "show table TableName=<database_table> policy update",
                PolicyResult);

        public static readonly CommandSymbol ShowTableStarPolicyUpdate =
            new CommandSymbol(nameof(ShowTableStarPolicyUpdate),
                "show table '*' policy update",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicyUpdate =
            new CommandSymbol(nameof(AlterTablePolicyUpdate),
                "alter table TableName=<database_table> policy update UpdatePolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeTablePolicyUpdate =
            new CommandSymbol(nameof(AlterMergeTablePolicyUpdate),
                "alter-merge table TableName=<database_table> policy update UpdatePolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicyUpdate =
            new CommandSymbol(nameof(DeleteTablePolicyUpdate),
                "delete table TableName=<database_table> policy update",
                PolicyResult);
        #endregion

        #region IngestionBatching
        public static readonly CommandSymbol ShowClusterPolicyIngestionBatching =
            new CommandSymbol(nameof(ShowClusterPolicyIngestionBatching),
                "show cluster policy ingestionbatching",
                PolicyResult);

        public static readonly CommandSymbol ShowDatabasePolicyIngestionBatching =
            new CommandSymbol(nameof(ShowDatabasePolicyIngestionBatching),
                "show database DatabaseName=(<database> | '*') policy ingestionbatching",
                PolicyResult);

        public static readonly CommandSymbol ShowTablePolicyIngestionBatching =
            new CommandSymbol(nameof(ShowTablePolicyIngestionBatching),
                "show table TableName=<database_table> policy ingestionbatching",
                PolicyResult);

        public static readonly CommandSymbol ShowTableStarPolicyIngestionBatching =
            new CommandSymbol(nameof(ShowTableStarPolicyIngestionBatching),
                "show table '*' policy ingestionbatching",
                PolicyResult);

        public static readonly CommandSymbol AlterClusterPolicyIngestionBatching =
            new CommandSymbol(nameof(AlterClusterPolicyIngestionBatching),
                "alter cluster policy ingestionbatching IngestionBatchingPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterDatabasePolicyIngestionBatching =
            new CommandSymbol(nameof(AlterDatabasePolicyIngestionBatching),
                "alter database DatabaseName=<database> policy ingestionbatching IngestionBatchingPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicyIngestionBatching =
            new CommandSymbol(nameof(AlterTablePolicyIngestionBatching),
                "alter table TableName=<database_table> policy ingestionbatching IngestionBatchingPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterTablesPolicyIngestionBatching =
            new CommandSymbol(nameof(AlterTablesPolicyIngestionBatching),
                "alter tables '(' { TableName=<table>, ',' }+ ')' policy ingestionbatching IngestionBatchingPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteDatabasePolicyIngestionBatching =
            new CommandSymbol(nameof(DeleteDatabasePolicyIngestionBatching),
                "delete database DatabaseName=<database> policy ingestionbatching",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicyIngestionBatching =
            new CommandSymbol(nameof(DeleteTablePolicyIngestionBatching),
                "delete table TableName=<database_table> policy ingestionbatching",
                PolicyResult);
        #endregion

        #region Encoding
        public static readonly CommandSymbol ShowDatabasePolicyEncoding =
            new CommandSymbol(nameof(ShowDatabasePolicyEncoding),
                "show database DatabaseName=<database> policy encoding",
                PolicyResult);

        public static readonly CommandSymbol ShowTablePolicyEncoding =
            new CommandSymbol(nameof(ShowTablePolicyEncoding),
                "show table TableName=<database_table> policy encoding",
                PolicyResult);

        public static readonly CommandSymbol ShowColumnPolicyEncoding =
            new CommandSymbol(nameof(ShowColumnPolicyEncoding),
                "show column ColumnName=<table_column> policy encoding",
                PolicyResult);

        public static readonly CommandSymbol AlterDatabasePolicyEncoding =
            new CommandSymbol(nameof(AlterDatabasePolicyEncoding),
                "alter database DatabaseName=<database> policy encoding EncodingPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicyEncoding =
            new CommandSymbol(nameof(AlterTablePolicyEncoding),
                "alter table TableName=<database_table> policy encoding EncodingPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterTableColumnsPolicyEncoding =
            new CommandSymbol(nameof(AlterTableColumnsPolicyEncoding),
                "alter table TableName=<table> columns policy encoding EncodingPolicies=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterColumnPolicyEncoding =
            new CommandSymbol(nameof(AlterColumnPolicyEncoding),
                "alter column ColumnName=<database_table_column> policy encoding EncodingPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterColumnPolicyEncodingType =
            new CommandSymbol(nameof(AlterColumnPolicyEncodingType),
                "alter column ColumnName=<database_table_column> policy encoding type '=' EncodingPolicyType=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeDatabasePolicyEncoding =
            new CommandSymbol(nameof(AlterMergeDatabasePolicyEncoding),
                "alter-merge database DatabaseName=<database> policy encoding EncodingPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeTablePolicyEncoding =
            new CommandSymbol(nameof(AlterMergeTablePolicyEncoding),
                "alter-merge table TableName=<database_table> policy encoding EncodingPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeColumnPolicyEncoding =
            new CommandSymbol(nameof(AlterMergeColumnPolicyEncoding),
                "alter-merge column ColumnName=<table_column> policy encoding EncodingPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteDatabasePolicyEncoding =
            new CommandSymbol(nameof(DeleteDatabasePolicyEncoding),
                "delete database DatabaseName=<database> policy encoding",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicyEncoding =
            new CommandSymbol(nameof(DeleteTablePolicyEncoding),
                "delete table TableName=<database_table> policy encoding",
                PolicyResult);

        public static readonly CommandSymbol DeleteColumnPolicyEncoding =
            new CommandSymbol(nameof(DeleteColumnPolicyEncoding),
                "delete column ColumnName=<table_column> policy encoding",
                PolicyResult);
        #endregion

        #region Merge
        public static readonly CommandSymbol ShowDatabasePolicyMerge =
            new CommandSymbol(nameof(ShowDatabasePolicyMerge),
                "show database DatabaseName=(<database> | '*') policy merge",
                PolicyResult);

        public static readonly CommandSymbol ShowTablePolicyMerge =
            new CommandSymbol(nameof(ShowTablePolicyMerge),
                "show table TableName=<database_table> policy merge",
                PolicyResult);

        public static readonly CommandSymbol ShowTableStarPolicyMerge =
            new CommandSymbol(nameof(ShowTableStarPolicyMerge),
                "show table '*' policy merge",
                PolicyResult);

        public static readonly CommandSymbol AlterDatabasePolicyMerge =
            new CommandSymbol(nameof(AlterDatabasePolicyMerge),
                "alter database DatabaseName=<database> policy merge MergePolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicyMerge =
            new CommandSymbol(nameof(AlterTablePolicyMerge),
                "alter table TableName=<database_table> policy merge MergePolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterTablesPolicyMerge =
            new CommandSymbol(nameof(AlterTablesPolicyMerge),
                "alter tables '(' {TableName=<table>, ','}+ ')' policy merge policy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeDatabasePolicyMerge =
            new CommandSymbol(nameof(AlterMergeDatabasePolicyMerge),
                "alter-merge database DatabaseName=<database> policy merge MergePolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeTablePolicyMerge =
            new CommandSymbol(nameof(AlterMergeTablePolicyMerge),
                "alter-merge table TableName=<database_table> policy merge MergePolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteDatabasePolicyMerge =
            new CommandSymbol(nameof(DeleteDatabasePolicyMerge),
                "delete database DatabaseName=<database> policy merge",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicyMerge =
            new CommandSymbol(nameof(DeleteTablePolicyMerge),
                "delete table TableName=<database_table> policy merge",
                PolicyResult);
        #endregion

        #region Partitioning
        public static readonly CommandSymbol ShowTablePolicyPartitioning =
            new CommandSymbol(nameof(ShowTablePolicyPartitioning),
                "show table TableName=<database_table> policy partitioning",
                PolicyResult);

        public static readonly CommandSymbol ShowTableStarPolicyPartitioning =
            new CommandSymbol(nameof(ShowTableStarPolicyPartitioning),
                "show table '*' policy partitioning",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicyPartitioning =
            new CommandSymbol(nameof(AlterTablePolicyPartitioning),
                "alter table TableName=<table> policy partitioning Policy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeTablePolicyPartitioning =
            new CommandSymbol(nameof(AlterMergeTablePolicyPartitioning),
                "alter-merge table TableName=<table> policy partitioning Policy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMaterializedViewPolicyPartitioning =
            new CommandSymbol(nameof(AlterMaterializedViewPolicyPartitioning),
                "alter materialized-view MaterializedViewName=<materializedview> policy partitioning Policy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeMaterializedViewPolicyPartitioning =
            new CommandSymbol(nameof(AlterMergeMaterializedViewPolicyPartitioning),
                "alter-merge materialized-view MaterializedViewName=<materializedview> policy partitioning Policy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicyPartitioning =
            new CommandSymbol(nameof(DeleteTablePolicyPartitioning),
                "delete table TableName=<table> policy partitioning",
                PolicyResult);

        public static readonly CommandSymbol DeleteMaterializedViewPolicyPartitioning =
            new CommandSymbol(nameof(DeleteMaterializedViewPolicyPartitioning),
                "delete materialized-view MaterializedViewName=<materializedview> policy partitioning",
                PolicyResult);
        #endregion

        #region RestrictedViewAccess
        public static readonly CommandSymbol ShowTablePolicyRestrictedViewAccess =
            new CommandSymbol(nameof(ShowTablePolicyRestrictedViewAccess),
                "show table TableName=<database_table> policy restricted_view_access",
                PolicyResult);

        public static readonly CommandSymbol ShowTableStarPolicyRestrictedViewAccess =
            new CommandSymbol(nameof(ShowTableStarPolicyRestrictedViewAccess),
                "show table '*' policy restricted_view_access",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicyRestrictedViewAccess =
            new CommandSymbol(nameof(AlterTablePolicyRestrictedViewAccess),
                "alter table TableName=<database_table> policy restricted_view_access (true | false)",
                PolicyResult);

        public static readonly CommandSymbol AlterTablesPolicyRestrictedViewAccess =
            new CommandSymbol(nameof(AlterTablesPolicyRestrictedViewAccess),
                "alter tables '(' { TableName=<table>, ',' }+ ')' policy restricted_view_access (true | false)",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicyRestrictedViewAccess =
            new CommandSymbol(nameof(DeleteTablePolicyRestrictedViewAccess),
                "delete table TableName=<database_table> policy restricted_view_access",
                PolicyResult);
        #endregion

        #region RowStore
        public static readonly CommandSymbol ShowClusterPolicyRowStore =
            new CommandSymbol(nameof(ShowClusterPolicyRowStore),
                "show cluster policy rowstore",
                PolicyResult);

        public static readonly CommandSymbol AlterClusterPolicyRowStore =
            new CommandSymbol(nameof(AlterClusterPolicyRowStore),
                "alter cluster policy rowstore RowStorePolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeClusterPolicyRowStore =
            new CommandSymbol(nameof(AlterMergeClusterPolicyRowStore),
                "alter-merge cluster policy! rowstore RowStorePolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteClusterPolicyRowStore =
            new CommandSymbol(nameof(DeleteClusterPolicyRowStore),
                "delete cluster policy rowstore",
                PolicyResult);
        #endregion

        #region Sandbox
        public static readonly CommandSymbol ShowClusterPolicySandbox =
            new CommandSymbol(nameof(ShowClusterPolicySandbox),
                "show cluster policy sandbox",
                PolicyResult);

        public static readonly CommandSymbol AlterClusterPolicySandbox =
            new CommandSymbol(nameof(AlterClusterPolicySandbox),
                "alter cluster policy sandbox SandboxPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteClusterPolicySandbox =
            new CommandSymbol(nameof(DeleteClusterPolicySandbox),
                "delete cluster policy sandbox",
                PolicyResult);

        public static readonly CommandSymbol ShowClusterSandboxesStats =
            new CommandSymbol(nameof(ShowClusterSandboxesStats),
                "show cluster sandboxes stats",
                PolicyResult);

        #endregion

        #region Sharding
        public static readonly CommandSymbol ShowDatabasePolicySharding =
            new CommandSymbol(nameof(ShowDatabasePolicySharding),
                "show database DatabaseName=(<database> | '*') policy sharding",
                PolicyResult);

        public static readonly CommandSymbol ShowTablePolicySharding =
            new CommandSymbol(nameof(ShowTablePolicySharding),
                "show table TableName=<database_table> policy sharding",
                PolicyResult);

        public static readonly CommandSymbol ShowTableStarPolicySharding =
            new CommandSymbol(nameof(ShowTableStarPolicySharding),
                "show table '*' policy sharding",
                PolicyResult);

        public static readonly CommandSymbol AlterDatabasePolicySharding =
            new CommandSymbol(nameof(AlterDatabasePolicySharding),
                "alter database DatabaseName=<database> policy sharding ShardingPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicySharding =
            new CommandSymbol(nameof(AlterTablePolicySharding),
                "alter table TableName=<database_table> policy sharding ShardingPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeDatabasePolicySharding =
            new CommandSymbol(nameof(AlterMergeDatabasePolicySharding),
                "alter-merge database DatabaseName=<database> policy sharding ShardingPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeTablePolicySharding =
            new CommandSymbol(nameof(AlterMergeTablePolicySharding),
                "alter-merge table TableName=<database_table> policy sharding ShardingPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteDatabasePolicySharding =
            new CommandSymbol(nameof(DeleteDatabasePolicySharding),
                "delete database DatabaseName=<database> policy sharding",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicySharding =
            new CommandSymbol(nameof(DeleteTablePolicySharding),
                "delete table TableName=<database_table> policy sharding",
                PolicyResult);

        public static readonly CommandSymbol AlterClusterPolicySharding =
            new CommandSymbol(nameof(AlterClusterPolicySharding),
                "operationName=(alter | alter-merge) cluster policy sharding policy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteClusterPolicySharding =
            new CommandSymbol(nameof(DeleteClusterPolicySharding),
                "delete cluster policy sharding",
                PolicyResult);

        public static readonly CommandSymbol ShowClusterPolicySharding =
            new CommandSymbol(nameof(ShowClusterPolicySharding),
                $"show cluster policy sharding [{PropertyList()}]",
                PolicyResult);
        #endregion

        #region StreamingIngestion
        public static readonly CommandSymbol ShowDatabasePolicyStreamingIngestion =
            new CommandSymbol(nameof(ShowDatabasePolicyStreamingIngestion),
                "show database DatabaseName=<database> policy streamingingestion",
                PolicyResult);

        public static readonly CommandSymbol ShowTablePolicyStreamingIngestion =
            new CommandSymbol(nameof(ShowTablePolicyStreamingIngestion),
                "show table TableName=<database_table> policy streamingingestion",
                PolicyResult);

        public static readonly CommandSymbol ShowClusterPolicyStreamingIngestion =
            new CommandSymbol(nameof(ShowClusterPolicyStreamingIngestion),
                "show cluster policy streamingingestion",
                PolicyResult);

        public static readonly CommandSymbol AlterDatabasePolicyStreamingIngestion =
            new CommandSymbol(nameof(AlterDatabasePolicyStreamingIngestion),
                "alter database DatabaseName=<database> policy streamingingestion (StreamingIngestionPolicy=<string>|Status=(enable|disable))",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeDatabasePolicyStreamingIngestion =
            new CommandSymbol(nameof(AlterMergeDatabasePolicyStreamingIngestion),
                "alter-merge database DatabaseName=<database> policy streamingingestion StreamingIngestionPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicyStreamingIngestion =
            new CommandSymbol(nameof(AlterTablePolicyStreamingIngestion),
                "alter table TableName=<database_table> policy streamingingestion (StreamingIngestionPolicy=<string>|Status=(enable|disable))",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeTablePolicyStreamingIngestion =
            new CommandSymbol(nameof(AlterMergeTablePolicyStreamingIngestion),
                "alter-merge table TableName=<database_table> policy streamingingestion StreamingIngestionPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterClusterPolicyStreamingIngestion =
            new CommandSymbol(nameof(AlterClusterPolicyStreamingIngestion),
                "alter cluster policy streamingingestion StreamingIngestionPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeClusterPolicyStreamingIngestion =
            new CommandSymbol(nameof(AlterMergeClusterPolicyStreamingIngestion),
                "alter-merge cluster policy streamingingestion policy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteDatabasePolicyStreamingIngestion =
            new CommandSymbol(nameof(DeleteDatabasePolicyStreamingIngestion),
                "delete database DatabaseName=<database> policy streamingingestion",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicyStreamingIngestion =
            new CommandSymbol(nameof(DeleteTablePolicyStreamingIngestion),
                "delete table TableName=<database_table> policy streamingingestion",
                PolicyResult);

        public static readonly CommandSymbol DeleteClusterPolicyStreamingIngestion =
            new CommandSymbol(nameof(DeleteClusterPolicyStreamingIngestion),
                "delete cluster policy streamingingestion",
                PolicyResult);
        #endregion

        #region ManagedIdentity
        public static readonly CommandSymbol ShowDatabasePolicyManagedIdentity =
            new CommandSymbol(nameof(ShowDatabasePolicyManagedIdentity),
                "show database DatabaseName=<database> policy managed_identity",
                PolicyResult);

        public static readonly CommandSymbol ShowClusterPolicyManagedIdentity =
            new CommandSymbol(nameof(ShowClusterPolicyManagedIdentity),
                "show cluster policy managed_identity",
                PolicyResult);

        public static readonly CommandSymbol AlterDatabasePolicyManagedIdentity =
            new CommandSymbol(nameof(AlterDatabasePolicyManagedIdentity),
                "alter database DatabaseName=<database> policy managed_identity ManagedIdentityPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterClusterPolicyManagedIdentity =
            new CommandSymbol(nameof(AlterClusterPolicyManagedIdentity),
                "alter cluster policy managed_identity ManagedIdentityPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteDatabasePolicyManagedIdentity =
            new CommandSymbol(nameof(DeleteDatabasePolicyManagedIdentity),
                "delete database DatabaseName=<database> policy managed_identity",
                PolicyResult);

        public static readonly CommandSymbol DeleteClusterPolicyManagedIdentity =
            new CommandSymbol(nameof(DeleteClusterPolicyManagedIdentity),
                "delete cluster policy managed_identity",
                PolicyResult);

        #endregion

        #region AutoDelete

        public static readonly CommandSymbol ShowTablePolicyAutoDelete =
            new CommandSymbol(nameof(ShowTablePolicyAutoDelete),
                "show table TableName=(<database_table>) policy auto_delete",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicyAutoDelete =
            new CommandSymbol(nameof(AlterTablePolicyAutoDelete),
                "alter table TableName=<database_table> policy auto_delete AutoDeletePolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicyAutoDelete =
            new CommandSymbol(nameof(DeleteTablePolicyAutoDelete),
                "delete table TableName=<database_table> policy auto_delete",
                PolicyResult);

        #endregion 

        #region Callout
        public static readonly CommandSymbol ShowClusterPolicyCallout =
            new CommandSymbol(nameof(ShowClusterPolicyCallout),
                "show cluster policy callout",
                PolicyResult);

        public static readonly CommandSymbol AlterClusterPolicyCallout =
            new CommandSymbol(nameof(AlterClusterPolicyCallout),
                "alter cluster policy callout Policy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeClusterPolicyCallout =
            new CommandSymbol(nameof(AlterMergeClusterPolicyCallout),
                "alter-merge cluster policy callout Policy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteClusterPolicyCallout =
            new CommandSymbol(nameof(DeleteClusterPolicyCallout),
                "delete cluster policy callout",
                PolicyResult);
        #endregion

        #region Capacity
        public static readonly CommandSymbol ShowClusterPolicyCapacity =
            new CommandSymbol(nameof(ShowClusterPolicyCapacity),
                "show cluster policy capacity",
                PolicyResult);

        public static readonly CommandSymbol AlterClusterPolicyCapacity =
            new CommandSymbol(nameof(AlterClusterPolicyCapacity),
                "alter cluster policy capacity Policy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeClusterPolicyCapacity =
            new CommandSymbol(nameof(AlterMergeClusterPolicyCapacity),
                "alter-merge cluster policy capacity Policy=<string>",
                PolicyResult);
        #endregion

        #region Request classification
        public static readonly CommandSymbol ShowClusterPolicyRequestClassification =
            new CommandSymbol(nameof(ShowClusterPolicyRequestClassification),
                "show cluster policy request_classification",
                PolicyResult);

        public static readonly CommandSymbol AlterClusterPolicyRequestClassification =
            new CommandSymbol(nameof(AlterClusterPolicyRequestClassification),
                "alter cluster policy request_classification Policy=<string> '<|' Query=<input_query>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeClusterPolicyRequestClassification =
            new CommandSymbol(nameof(AlterMergeClusterPolicyRequestClassification),
                "alter-merge cluster policy request_classification Policy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteClusterPolicyRequestClassification =
                new CommandSymbol(nameof(DeleteClusterPolicyRequestClassification),
                "delete cluster policy request_classification",
                PolicyResult);
        #endregion

        #region Multi Database Admins
        public static readonly CommandSymbol ShowClusterPolicyMultiDatabaseAdmins =
            new CommandSymbol(nameof(ShowClusterPolicyMultiDatabaseAdmins),
                "show cluster policy multidatabaseadmins",
                PolicyResult);

        public static readonly CommandSymbol AlterClusterPolicyMultiDatabaseAdmins =
            new CommandSymbol(nameof(AlterClusterPolicyMultiDatabaseAdmins),
                "alter cluster policy multidatabaseadmins Policy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeClusterPolicyMultiDatabaseAdmins =
            new CommandSymbol(nameof(AlterMergeClusterPolicyMultiDatabaseAdmins),
                "alter-merge cluster policy multidatabaseadmins Policy=<string>",
                PolicyResult);

        #endregion

        #region Diagnostics
        public static readonly CommandSymbol ShowDatabasePolicyDiagnostics =
            new CommandSymbol(nameof(ShowDatabasePolicyDiagnostics),
                "show database DatabaseName=<database> policy diagnostics",
                PolicyResult);

        public static readonly CommandSymbol ShowClusterPolicyDiagnostics =
            new CommandSymbol(nameof(ShowClusterPolicyDiagnostics),
                "show cluster policy diagnostics",
                PolicyResult);

        public static readonly CommandSymbol AlterDatabasePolicyDiagnostics =
            new CommandSymbol(nameof(AlterDatabasePolicyDiagnostics),
                "alter database DatabaseName=<database> policy diagnostics PolicyName=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeDatabasePolicyDiagnostics =
            new CommandSymbol(nameof(AlterMergeDatabasePolicyDiagnostics),
                "alter-merge database DatabaseName=<database> policy diagnostics PolicyName=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterClusterPolicyDiagnostics =
            new CommandSymbol(nameof(AlterClusterPolicyDiagnostics),
                "alter cluster policy diagnostics PolicyName=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeClusterPolicyDiagnostics =
            new CommandSymbol(nameof(AlterMergeClusterPolicyDiagnostics),
                "alter-merge cluster policy diagnostics PolicyName=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteDatabasePolicyDiagnostics =
            new CommandSymbol(nameof(DeleteDatabasePolicyDiagnostics),
                "delete database DatabaseName=<database> policy diagnostics",
                PolicyResult);
        #endregion

        #region Weak Consistency Query
        public static readonly CommandSymbol ShowClusterPolicyQueryWeakConsistency =
            new CommandSymbol(nameof(ShowClusterPolicyQueryWeakConsistency),
                "show cluster policy query_weak_consistency",
                PolicyResult);

        public static readonly CommandSymbol AlterClusterPolicyQueryWeakConsistency =
            new CommandSymbol(nameof(AlterClusterPolicyQueryWeakConsistency),
                "alter cluster policy query_weak_consistency Policy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterMergeClusterPolicyQueryWeakConsistency =
            new CommandSymbol(nameof(AlterMergeClusterPolicyQueryWeakConsistency),
                "alter-merge cluster policy query_weak_consistency Policy=<string>",
                PolicyResult);
        #endregion

        #region Extent Tags Retention
        public static readonly CommandSymbol ShowTablePolicyExtentTagsRetention =
            new CommandSymbol(nameof(ShowTablePolicyExtentTagsRetention),
                "show table TableName=<database_table> policy extent_tags_retention",
                PolicyResult);

        public static readonly CommandSymbol ShowTableStarPolicyExtentTagsRetention =
            new CommandSymbol(nameof(ShowTableStarPolicyExtentTagsRetention),
                "show table '*' policy extent_tags_retention",
                PolicyResult);

        public static readonly CommandSymbol ShowDatabasePolicyExtentTagsRetention =
            new CommandSymbol(nameof(ShowDatabasePolicyExtentTagsRetention),
                "show database DatabaseName=(<database> | '*') policy extent_tags_retention",
                PolicyResult);

        public static readonly CommandSymbol AlterTablePolicyExtentTagsRetention =
            new CommandSymbol(nameof(AlterTablePolicyExtentTagsRetention),
                "alter table TableName=<database_table> policy extent_tags_retention ExtentTagsRetentionPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol AlterDatabasePolicyExtentTagsRetention =
            new CommandSymbol(nameof(AlterDatabasePolicyExtentTagsRetention),
                "alter database DatabaseName=<database> policy extent_tags_retention ExtentTagsRetentionPolicy=<string>",
                PolicyResult);

        public static readonly CommandSymbol DeleteTablePolicyExtentTagsRetention =
            new CommandSymbol(nameof(DeleteTablePolicyExtentTagsRetention),
                "delete table TableName=<database_table> policy extent_tags_retention",
                PolicyResult);

        public static readonly CommandSymbol DeleteDatabasePolicyExtentTagsRetention =
            new CommandSymbol(nameof(DeleteDatabasePolicyExtentTagsRetention),
                "delete database DatabaseName=<database> policy extent_tags_retention",
                PolicyResult);
        #endregion
        #endregion

        #region Security Role Commands

        private static readonly string ShowPrincipalRolesResult =
            "(Scope: string, DisplayName: string, AADObjectID: string, Role: string)";

        public static readonly CommandSymbol ShowPrincipalRoles =
            new CommandSymbol(nameof(ShowPrincipalRoles),
                $"show principal (roles | Principal=<string> roles) [{PropertyList()}]",
                ShowPrincipalRolesResult);

        public static readonly CommandSymbol ShowDatabasePrincipalRoles =
            new CommandSymbol(nameof(ShowDatabasePrincipalRoles),
                $"show database DatabaseName=<database> principal (roles | Principal=<string> roles) [{PropertyList()}]",
                ShowPrincipalRolesResult);

        public static readonly CommandSymbol ShowTablePrincipalRoles =
            new CommandSymbol(nameof(ShowTablePrincipalRoles),
                $"show table TableName=<table> principal (roles | Principal=<string> roles) [{PropertyList()}]",
                ShowPrincipalRolesResult);

        public static readonly CommandSymbol ShowExternalTablesPrincipalRoles =
            new CommandSymbol(nameof(ShowExternalTablesPrincipalRoles),
                $"show external table ExternalTableName=<externaltable> principal (roles | Principal=<string> roles) [{PropertyList()}]",
                ShowPrincipalRolesResult);

        public static readonly CommandSymbol ShowFunctionPrincipalRoles =
            new CommandSymbol(nameof(ShowFunctionPrincipalRoles),
                $"show function FunctionName=<function> principal (roles | Principal=<string> roles) [{PropertyList()}]",
                ShowPrincipalRolesResult);

        public static readonly CommandSymbol ShowClusterPrincipalRoles =
            new CommandSymbol(nameof(ShowClusterPrincipalRoles),
                $"show cluster principal (roles | Principal=<string> roles) [{PropertyList()}]",
                ShowPrincipalRolesResult);

        private static readonly string ShowPrincipalsResult =
            "(Role: string, PrincipalType: string, PrincipalDisplayName: string, PrincipalObjectId: string, PrincipalFQN: string, Notes: string, RoleAssignmentIdentifier: string)";

        public static readonly CommandSymbol ShowClusterPrincipals =
            new CommandSymbol(nameof(ShowClusterPrincipals),
                "show cluster principals",
                ShowPrincipalsResult);

        public static readonly CommandSymbol ShowDatabasePrincipals =
            new CommandSymbol(nameof(ShowDatabasePrincipals),
                "show database DatabaseName=<database> principals",
                ShowPrincipalsResult);

        public static readonly CommandSymbol ShowTablePrincipals =
            new CommandSymbol(nameof(ShowTablePrincipals),
                "show table TableName=<table> principals",
                ShowPrincipalsResult);

        public static readonly CommandSymbol ShowFunctionPrincipals =
            new CommandSymbol(nameof(ShowFunctionPrincipals),
                "show function FunctionName=<function> principals",
                ShowPrincipalsResult);

        private static string ClusterRole = "Role=(admins | databasecreators | users | viewers)";
        private static string DatabaseRole = "Role=(admins | ingestors | monitors | unrestrictedviewers | users | viewers)";
        private static string TableRole = "Role=(admins | ingestors)";
        private static string FunctionRole = "Role=admins";
        private static string PrincipalsClause = "'(' { Principal=<string>, ',' }+ ')' [SkipResults=skip-results] [Notes=<string>]";
        private static string PrincipalsOrNoneClause = $"(none [SkipResults=skip-results] | {PrincipalsClause})";

        public static readonly CommandSymbol AddClusterRole =
            new CommandSymbol(nameof(AddClusterRole),
                $"add cluster {ClusterRole} {PrincipalsClause}",
                ShowPrincipalsResult);

        public static readonly CommandSymbol DropClusterRole =
            new CommandSymbol(nameof(DropClusterRole),
                $"drop cluster {ClusterRole} {PrincipalsClause}",
                ShowPrincipalsResult);

        public static readonly CommandSymbol SetClusterRole =
            new CommandSymbol(nameof(SetClusterRole),
                $"set cluster {ClusterRole} {PrincipalsOrNoneClause}",
                ShowPrincipalsResult);

        public static readonly CommandSymbol AddDatabaseRole =
            new CommandSymbol(nameof(AddDatabaseRole),
                $"add database DatabaseName=<database> {DatabaseRole} {PrincipalsClause}",
                ShowPrincipalsResult);

        public static readonly CommandSymbol DropDatabaseRole =
            new CommandSymbol(nameof(DropDatabaseRole),
                $"drop database DatabaseName=<database> {DatabaseRole} {PrincipalsClause}",
                ShowPrincipalsResult);

        public static readonly CommandSymbol SetDatabaseRole =
            new CommandSymbol(nameof(SetDatabaseRole),
                $"set database DatabaseName=<database> {DatabaseRole} {PrincipalsOrNoneClause}",
                ShowPrincipalsResult);

        public static readonly CommandSymbol AddTableRole =
            new CommandSymbol(nameof(AddTableRole),
                $"add table TableName=<table> {TableRole} {PrincipalsClause}",
                ShowPrincipalsResult);

        public static readonly CommandSymbol DropTableRole =
            new CommandSymbol(nameof(DropTableRole),
                $"drop table TableName=<table> {TableRole} {PrincipalsClause}",
                ShowPrincipalsResult);

        public static readonly CommandSymbol SetTableRole =
            new CommandSymbol(nameof(SetTableRole),
                $"set table TableName=<table> {TableRole} {PrincipalsOrNoneClause}",
                ShowPrincipalsResult);

        public static readonly CommandSymbol AddFunctionRole =
            new CommandSymbol(nameof(AddFunctionRole),
                $"add function FunctionName=<function> {FunctionRole} {PrincipalsClause}",
                ShowPrincipalsResult);

        public static readonly CommandSymbol DropFunctionRole =
            new CommandSymbol(nameof(DropFunctionRole),
                $"drop function FunctionName=<function> {FunctionRole} {PrincipalsClause}",
                ShowPrincipalsResult);

        public static readonly CommandSymbol SetFunctionRole =
            new CommandSymbol(nameof(SetFunctionRole),
                $"set function FunctionName=<function> {FunctionRole} {PrincipalsOrNoneClause}",
                ShowPrincipalsResult);

        private static readonly string BlockedPrincipalsResult =
            "(PrincipalType: string, PrincipalDisplayName: string, PrincipalObjectId: string, PrincipalFQN: string, Application: string, User: string, BlockedUntil: datetime, Reason: string)";

        public static readonly CommandSymbol ShowClusterBlockedPrincipals =
            new CommandSymbol(nameof(ShowClusterBlockedPrincipals),
                "show cluster blockedprincipals",
                BlockedPrincipalsResult);

        public static readonly CommandSymbol AddClusterBlockedPrincipals =
            new CommandSymbol(nameof(AddClusterBlockedPrincipals),
                "add cluster blockedprincipals Principal=<string> [application AppName=<string>] [user UserName=<string>] [period Period=<timespan>] [reason Reason=<string>]",
                BlockedPrincipalsResult);

        public static readonly CommandSymbol DropClusterBlockedPrincipals =
            new CommandSymbol(nameof(DropClusterBlockedPrincipals),
                "drop cluster blockedprincipals Principal=<string> [application AppName=<string>] [user UserName=<string>]",
                BlockedPrincipalsResult);
        #endregion

        #region Data Ingestion
        private static readonly string PathOrPathList = "(Path=<string> | '(' { Path=<string>, ',' }+ ')')";

        private static string PropertyList(string propertyNameRule = null) =>
            string.IsNullOrEmpty(propertyNameRule)
                ? "with '('! { PropertyName=<name> '='! PropertyValue=<value>, ',' }+ ')'"
                : $"with '('! {{ PropertyName=({propertyNameRule} | <name>) '='! PropertyValue=<value>, ',' }}+ ')'";

        private static readonly string DataIngestionPropertyList =
            PropertyList(
                "ingestionMapping | ingestionMappingReference | creationTime | distributed | docstring | extend_schema | folder | format | ingestIfNotExists | " +
                "ignoreFirstRecord | persistDetails | policy_ingestionTime | recreate_schema | tags | validationPolicy | zipPattern");

        public static readonly CommandSymbol IngestIntoTable =
            new CommandSymbol(nameof(IngestIntoTable),
                $"ingest [async] into table! TableName=<table> {PathOrPathList} [{DataIngestionPropertyList}]",
                "(ExtentId: guid, ItemLoaded: string, Duration: string, HasErrors: string, OperationId: guid)");

        public static readonly CommandSymbol IngestInlineIntoTable =
            new CommandSymbol(nameof(IngestInlineIntoTable),
                $"ingest inline into! table TableName=<name> ('[' Data=<bracketed_input_data> ']' | {DataIngestionPropertyList} '<|'! Data=<input_data> | '<|' Data=<input_data>)",
                "(ExtentId: guid)");

        private static readonly string DataIngestionSetAppendResult =
            "(ExtentId: guid, OriginalSize: long, ExtentSize: long, ColumnSize: long, IndexSize: long, RowCount: long)";

        public static readonly CommandSymbol SetTable =
            new CommandSymbol(nameof(SetTable),
                $"set [async] TableName=<name> [{DataIngestionPropertyList}] '<|' QueryOrCommand=<input_query>",
                DataIngestionSetAppendResult);

        public static readonly CommandSymbol AppendTable =
            new CommandSymbol(nameof(AppendTable),
                $"append [async] TableName=<table> [{DataIngestionPropertyList}] '<|' QueryOrCommand=<input_query>",
                DataIngestionSetAppendResult);

        public static readonly CommandSymbol SetOrAppendTable =
            new CommandSymbol(nameof(SetOrAppendTable),
                $"set-or-append [async] TableName=<name> [{DataIngestionPropertyList}] '<|' QueryOrCommand=<input_query>",
                DataIngestionSetAppendResult);

        public static readonly CommandSymbol SetOrReplaceTable =
            new CommandSymbol(nameof(SetOrReplaceTable),
                $"set-or-replace [async] TableName=<name> [{DataIngestionPropertyList}] '<|' QueryOrCommand=<input_query>",
                DataIngestionSetAppendResult);
        #endregion

        #region Data Export
        private static string DataConnectionStringList = "'(' { DataConnectionString=<string>, ',' }+ ')'";

        public static readonly CommandSymbol ExportToStorage =
            new CommandSymbol(nameof(ExportToStorage),
                $"export [async] [compressed] to (csv|tsv|json|parquet) {DataConnectionStringList} [{PropertyList()}] '<|' Query=<input_query>",
                UnknownResult);

        public static readonly CommandSymbol ExportToSqlTable =
            new CommandSymbol(nameof(ExportToSqlTable),
                $"export [async] to sql SqlTableName=<name> SqlConnectionString=<string> [{PropertyList()}] '<|' Query=<input_query>",
                UnknownResult);

        public static readonly CommandSymbol ExportToExternalTable =
            new CommandSymbol(nameof(ExportToExternalTable),
                $"export [async] to table ExternalTableName=<externaltable> [{PropertyList()}] '<|' Query=<input_query>",
                UnknownResult);

        private static readonly string OverClause = "over '('! { TableName=<name>, ',' }+ ')'";

        public static readonly CommandSymbol CreateOrAlterContinuousExport =
            new CommandSymbol(nameof(CreateOrAlterContinuousExport),
                $"create-or-alter continuous-export ContinuousExportName=<name> [{OverClause}] to table ExternalTableName=<externaltable> [{PropertyList()}] '<|' Query=<input_query>",
                UnknownResult);

        private static readonly string ShowContinuousExportResult =
            "(Name: string, ExternalTableName: string, Query: string, " +
            "ForcedLatency: timespan, IntervalBetweenRuns: timespan, CursorScopedTables: string, ExportProperties: string, " +
            "LastRunTime: datetime, StartCursor: string, IsDisabled: bool, LastRunResult: string, ExportedTo: datetime, IsRunning: bool)";

        public static readonly CommandSymbol ShowContinuousExport =
            new CommandSymbol(nameof(ShowContinuousExport),
                "show continuous-export ContinuousExportName=<name>",
                ShowContinuousExportResult);

        public static readonly CommandSymbol ShowContinuousExports =
            new CommandSymbol(nameof(ShowContinuousExports),
                "show continuous-exports",
                ShowContinuousExportResult);

        public static readonly CommandSymbol ShowClusterPendingContinuousExports =
            new CommandSymbol(nameof(ShowClusterPendingContinuousExports),
                $"show cluster pending continuous-exports [{PropertyList()}]",
                ShowContinuousExportResult);

        public static readonly CommandSymbol ShowContinuousExportExportedArtifacts =
            new CommandSymbol(nameof(ShowContinuousExportExportedArtifacts),
                "show continuous-export ContinuousExportName=<name> exported-artifacts",
                "(Timestamp: datetime, ExternalTableName: string, Path: string, NumRecords: long)");

        public static readonly CommandSymbol ShowContinuousExportFailures =
            new CommandSymbol(nameof(ShowContinuousExportFailures),
                "show continuous-export ContinuousExportName=<name> failures",
                "(Timestamp: datetime, OperationId: string, Name: string, LastSuccessRun: datetime, FailureKind: string, Details: string)");

        public static readonly CommandSymbol SetContinuousExportCursor =
            new CommandSymbol(nameof(SetContinuousExportCursor),
                "set continuous-export jobName=<name> cursor to cursorValue=<string>",
                UnknownResult);

        public static readonly CommandSymbol DropContinuousExport =
            new CommandSymbol(nameof(DropContinuousExport),
                "drop continuous-export ContinuousExportName=<name>",
                ShowContinuousExportResult);

        public static readonly CommandSymbol EnableContinuousExport =
            new CommandSymbol(nameof(EnableContinuousExport),
                "enable continuous-export ContinuousExportName=<name>",
                ShowContinuousExportResult);

        public static readonly CommandSymbol DisableContinuousExport =
            new CommandSymbol(nameof(DisableContinuousExport),
                "disable continuous-export ContinousExportName=<name>",
                ShowContinuousExportResult);

        #endregion

        #region Materialized Views

        private static readonly string MaterializedViewCreatePropertyList =
            PropertyList("lookback | backfill | effectiveDateTime | updateExtentsCreationTime | autoUpdateSchema | dimensionTables | folder | docString");

        private static readonly string MaterializedViewAlterPropertyList =
            PropertyList("lookback | dimensionTables");

        private static readonly string ShowMaterializedViewResult =
           "(Name: string, SourceTable: string, Query: string, " +
           "MaterializedTo: datetime, LastRun: datetime, LastRunResult: string, IsHealthy: bool, " +
           "IsEnabled: bool, Folder: string, DocString: string, AutoUpdateSchema: bool, EffectiveDateTime: datetime, Lookback:timespan)";

        private static readonly string ShowMaterializedViewsDetailsResult =
            "(MaterializedViewName: string, DatabaseName: string, Folder: string, DocString: string, TotalExtents: long, TotalExtentSize: real, TotalOriginalSize: real, TotalRowCount: long, HotExtents: long, HotExtentSize: real, HotOriginalSize: real, HotRowCount: long, AuthorizedPrincipals: string, RetentionPolicy: string, CachingPolicy: string, ShardingPolicy: string, MergePolicy: string, MinExtentsCreationTime: datetime, MaxExtentsCreationTime: datetime)";

        public static readonly CommandSymbol CreateMaterializedView =
            new CommandSymbol(nameof(CreateMaterializedView),
                $"create [async] materialized-view [{MaterializedViewCreatePropertyList}] " +
                "MaterializedViewName=<name> on table <table> <function_body>",
                UnknownResult);

        public static readonly CommandSymbol RenameMaterializedView =
            new CommandSymbol(nameof(RenameMaterializedView),
                "rename materialized-view MaterializedViewName=<materializedview> to NewMaterializedViewName=<name>",
                ShowMaterializedViewResult);

        public static readonly CommandSymbol ShowMaterializedView =
           new CommandSymbol(nameof(ShowMaterializedView),
               "show materialized-view MaterializedViewName=<materializedview>",
               ShowMaterializedViewResult);

        public static readonly CommandSymbol ShowMaterializedViews =
           new CommandSymbol(nameof(ShowMaterializedViews),
               "show materialized-views",
               ShowMaterializedViewResult);

        public static readonly CommandSymbol ShowMaterializedViewsDetails =
            new CommandSymbol(nameof(ShowMaterializedViewsDetails),
                "show materialized-views ['(' { MaterializedViewName=<materializedview>, ',' }+ ')'] details",
                ShowMaterializedViewsDetailsResult);

        public static readonly CommandSymbol ShowMaterializedViewDetails =
            new CommandSymbol(nameof(ShowMaterializedViewDetails),
                "show materialized-view MaterializedViewName=<materializedview> details",
                ShowMaterializedViewsDetailsResult);

        public static readonly CommandSymbol ShowMaterializedViewPolicyRetention =
            new CommandSymbol(nameof(ShowMaterializedViewPolicyRetention),
                "show materialized-view MaterializedViewName=<materializedview> policy retention",
                PolicyResult);

        public static readonly CommandSymbol ShowMaterializedViewPolicyMerge =
            new CommandSymbol(nameof(ShowMaterializedViewPolicyMerge),
                "show materialized-view MaterializedViewName=<materializedview> policy merge",
                PolicyResult);

        public static readonly CommandSymbol ShowMaterializedViewPolicyPartitioning =
           new CommandSymbol(nameof(ShowMaterializedViewPolicyPartitioning),
               "show materialized-view MaterializedViewName=<materializedview> policy partitioning",
               PolicyResult);

        public static readonly CommandSymbol ShowMaterializedViewExtents =
            new CommandSymbol(nameof(ShowMaterializedViewExtents),
                 $"show materialized-view MaterializedViewName=<materializedview> extents [{ExtentIdList}] [hot]",
                "(ExtentId: guid, DatabaseName: string, TableName: string, MaxCreatedOn: datetime, OriginalSize: real, ExtentSize: real, CompressedSize: real, IndexSize: real, Blocks: long, Segments: long, ExtentContainerId: string, RowCount: long, MinCreatedOn: datetime, Tags: string, Kind: string, DeletedRowCount: long)");

        public static readonly CommandSymbol AlterMaterializedView =
            new CommandSymbol(nameof(AlterMaterializedView),
                $"alter materialized-view [{MaterializedViewAlterPropertyList}] MaterializedViewName=<materializedview> on table <table> <function_body>",
                ShowMaterializedViewResult);

        public static readonly CommandSymbol CreateOrAlterMaterializedView =
            new CommandSymbol(nameof(CreateOrAlterMaterializedView),
                $"create-or-alter materialized-view [{MaterializedViewCreatePropertyList}] MaterializedViewName=<materializedview> on table <table> <function_body>",
                ShowMaterializedViewResult);

        public static readonly CommandSymbol DropMaterializedView =
            new CommandSymbol(nameof(DropMaterializedView),
                $"drop materialized-view MaterializedViewName=<materializedview>",
                ShowMaterializedViewResult);

        public static readonly CommandSymbol EnableDisableMaterializedView =
            new CommandSymbol(nameof(EnableDisableMaterializedView),
                $"(enable | disable) materialized-view MaterializedViewName=<materializedview>",
                ShowMaterializedViewResult);

        public static readonly CommandSymbol ShowMaterializedViewPrincipals =
            new CommandSymbol(nameof(ShowMaterializedViewPrincipals),
                "show materialized-view MaterializedViewName=<materializedview> principals",
                ShowPrincipalsResult);

        public static readonly CommandSymbol ShowMaterializedViewSchemaAsJson =
            new CommandSymbol(nameof(ShowMaterializedViewSchemaAsJson),
                "show materialized-view MaterializedViewName=<materializedview> schema as json",
                ShowTableSchemaResult);

        public static readonly CommandSymbol ShowMaterializedViewCslSchema =
            new CommandSymbol(nameof(ShowMaterializedViewCslSchema),
                "show materialized-view MaterializedViewName=<materializedview> cslschema",
                ShowTableSchemaResult);

        public static readonly CommandSymbol AlterMaterializedViewFolder =
            new CommandSymbol(nameof(AlterMaterializedViewFolder),
                "alter materialized-view MaterializedViewName=<materializedview> folder Folder=<string>",
                ShowMaterializedViewResult);

        public static readonly CommandSymbol AlterMaterializedViewDocString =
            new CommandSymbol(nameof(AlterMaterializedViewDocString),
                "alter materialized-view MaterializedViewName=<materializedview> docstring Documentation=<string>",
                ShowMaterializedViewResult);

        public static readonly CommandSymbol AlterMaterializedViewLookback =
            new CommandSymbol(nameof(AlterMaterializedViewLookback),
                "alter materialized-view MaterializedViewName=<materializedview> lookback Lookback=<timespan>",
                ShowMaterializedViewResult);

        public static readonly CommandSymbol AlterMaterializedViewAutoUpdateSchema =
            new CommandSymbol(nameof(AlterMaterializedViewAutoUpdateSchema),
                "alter materialized-view MaterializedViewName=<materializedview> autoUpdateSchema (true|false)",
                ShowMaterializedViewResult);

        public static readonly CommandSymbol ClearMaterializedViewData =
           new CommandSymbol(nameof(ClearMaterializedViewData),
               "clear materialized-view MaterializedViewName=<materializedview> data",
               "(ExtentId: guid, TableName: string, CreatedOn: datetime)");

        public static readonly CommandSymbol SetMaterializedViewCursor =
            new CommandSymbol(nameof(SetMaterializedViewCursor),
                "set materialized-view MaterializedViewName=<materializedview> cursor to CursorValue=<string>",
                UnknownResult);

        #endregion

        #region System Information Commands
        public static readonly CommandSymbol ShowCluster =
            new CommandSymbol(nameof(ShowCluster),
                "show cluster",
                "(NodeId: string, Address: string, Name: string, StartTime: string, IsAdmin: bool, " +
                    "MachineTotalMemory: long, MachineAvailableMemory: long, ProcessorCount: int, EnvironmentDescription: string)");

        public static readonly CommandSymbol ShowDiagnostics =
            new CommandSymbol(nameof(ShowDiagnostics),
                "show diagnostics [with '(' scope '=' Scope=(cluster | workloadgroup) ')']",
                new TableSymbol(
                    new ColumnSymbol("IsHealthy", ScalarTypes.Bool),
                    new ColumnSymbol("EnvironmentDescription", ScalarTypes.String),
                    new ColumnSymbol("IsScaleOutRequired", ScalarTypes.Bool),
                    new ColumnSymbol("MachinesTotal", ScalarTypes.Int),
                    new ColumnSymbol("MachinesOffline", ScalarTypes.Int),
                    new ColumnSymbol("NodeLastRestartedOn", ScalarTypes.DateTime),
                    new ColumnSymbol("AdminLastElectedOn", ScalarTypes.DateTime),
                    // snew ColumnSymbol("ReservedSlot2", ScalarTypes.Real),
                    new ColumnSymbol("ExtentsTotal", ScalarTypes.Int),
                    new ColumnSymbol("DiskColdAllocationPercentage", ScalarTypes.Int),
                    new ColumnSymbol("InstancesTargetBasedOnDataCapacity", ScalarTypes.Int),
                    new ColumnSymbol("TotalOriginalDataSize", ScalarTypes.Real),
                    new ColumnSymbol("TotalExtentSize", ScalarTypes.Real),
                    new ColumnSymbol("IngestionsLoadFactor", ScalarTypes.Real),
                    new ColumnSymbol("IngestionsInProgress", ScalarTypes.Long),
                    new ColumnSymbol("IngestionsSuccessRate", ScalarTypes.Real),
                    new ColumnSymbol("MergesInProgress", ScalarTypes.Long),
                    new ColumnSymbol("BuildVersion", ScalarTypes.String),
                    new ColumnSymbol("BuildTime", ScalarTypes.DateTime),
                    new ColumnSymbol("ClusterDataCapacityFactor", ScalarTypes.Real),
                    new ColumnSymbol("IsDataWarmingRequired", ScalarTypes.Bool),
                    // new ColumnSymbol("ReservedSlot3", ScalarTypes.String),
                    new ColumnSymbol("DataWarmingLastRunOn", ScalarTypes.DateTime),
                    new ColumnSymbol("MergesSuccessRate", ScalarTypes.Real),
                    new ColumnSymbol("NotHealthyReason", ScalarTypes.String),
                    new ColumnSymbol("IsAttentionRequired", ScalarTypes.Bool),
                    new ColumnSymbol("AttentionRequiredReason", ScalarTypes.String),
                    new ColumnSymbol("ProductVersion", ScalarTypes.String),
                    new ColumnSymbol("FailedIngestOperations", ScalarTypes.Int),
                    new ColumnSymbol("FailedMergeOperations", ScalarTypes.Int),
                    new ColumnSymbol("MaxExtentsInSingleTable", ScalarTypes.Int),
                    new ColumnSymbol("TableWithMaxExtents", ScalarTypes.String),
                    new ColumnSymbol("WarmExtentSize", ScalarTypes.Real),
                    new ColumnSymbol("NumberOfDatabases", ScalarTypes.Int),
                    new ColumnSymbol("PurgeExtentsRebuildLoadFactor", ScalarTypes.Real),
                    new ColumnSymbol("PurgeExtentsRebuildInProgress", ScalarTypes.Long),
                    new ColumnSymbol("PurgesInProgress", ScalarTypes.Long),
                    new ColumnSymbol("MaxSoftRetentionPolicyViolation", ScalarTypes.TimeSpan),
                    // new ColumnSymbol("ReservedSlot4", ScalarTypes.String),
                    new ColumnSymbol("RowStoreLocalStorageCapacityFactor", ScalarTypes.Real),
                    new ColumnSymbol("ExportsLoadFactor", ScalarTypes.Real),
                    new ColumnSymbol("ExportsInProgress", ScalarTypes.Long),
                    new ColumnSymbol("PendingContinuousExports", ScalarTypes.Long),
                    new ColumnSymbol("MaxContinuousExportLatenessMinutes", ScalarTypes.Long),
                    new ColumnSymbol("RowStoreSealsInProgress", ScalarTypes.Long),
                    new ColumnSymbol("IsRowStoreUnhealthy", ScalarTypes.Bool),
                    new ColumnSymbol("MachinesSuspended", ScalarTypes.Int),
                    new ColumnSymbol("DataPartitioningLoadFactor", ScalarTypes.Real),
                    new ColumnSymbol("DataPartitioningOperationsInProgress", ScalarTypes.Long),
                    new ColumnSymbol("MinPartitioningPercentageInSingleTable", ScalarTypes.Real),
                    new ColumnSymbol("TableWithMinPartitioningPercentage", ScalarTypes.String),
                    new ColumnSymbol("V2DataCapacityFactor", ScalarTypes.Real),
                    new ColumnSymbol("V3DataCapacityFactor", ScalarTypes.Real),
                    new ColumnSymbol("CurrentDiskCacheShardsPercentage", ScalarTypes.Int),
                    new ColumnSymbol("TargetDiskCacheShardsPercentage", ScalarTypes.Int),
                    new ColumnSymbol("MaterializedViewsInProgress", ScalarTypes.Long),
                    new ColumnSymbol("DataPartitioningOperationsInProgress", ScalarTypes.Real),
                    new ColumnSymbol("IngestionCapacityUtilization", ScalarTypes.Real),
                    new ColumnSymbol("ShardsWarmingStatus", ScalarTypes.String),
                    new ColumnSymbol("ShardsWarmingTemperature", ScalarTypes.Real),
                    new ColumnSymbol("ShardsWarmingDetails", ScalarTypes.String),
                    new ColumnSymbol("StoredQueryResultsInProgress", ScalarTypes.Long),
                    new ColumnSymbol("HotDataDiskSpaceUsage", ScalarTypes.Real)
                ));

        public static readonly CommandSymbol ShowCapacity =
            new CommandSymbol(nameof(ShowCapacity),
                "show capacity [with '(' scope '=' Scope=(cluster | workloadgroup) ')']",
                "(Resource: string, Total: long, Consumed: long, Remaining: long)");

        public static readonly CommandSymbol ShowOperations =
            new CommandSymbol(nameof(ShowOperations),
                "show operations [(OperationId=<guid> | '(' { OperationId=<guid>, ',' }+ ')')]",
                "(OperationId: guid, Operation: string, NodeId: string, StartedOn: datetime, LastUpdatedOn: datetime, Duration: timespan, State: string, Status: string, RootActivityId: guid, ShouldRetry: bool, Database: string, Principal: string, User: string, AdminEpochStartTime: datetime)");

        public static readonly CommandSymbol ShowOperationDetails =
            new CommandSymbol(nameof(ShowOperationDetails),
                "show operation OperationId=<guid> details",
                UnknownResult); // schema depends on operation

        private static readonly string JournalResult =
            "(Event: string, EventTimestamp: datetime, Database: string, EntityName: string, UpdatedEntityName: string, EntityVersion: string, EntityContainerName: string, " +
            "OriginalEntityState: string, UpdatedEntityState: string, ChangeCommand: string, Principal: string, RootActivityId: guid, ClientRequestId: string, " +
            "User: string, OriginalEntityVersion: string)";

        public static readonly CommandSymbol ShowJournal =
            new CommandSymbol(nameof(ShowJournal),
                "show journal",
                JournalResult);

        public static readonly CommandSymbol ShowDatabaseJournal =
            new CommandSymbol(nameof(ShowDatabaseJournal),
                "show database DatabaseName=<database> journal",
                JournalResult);

        public static readonly CommandSymbol ShowClusterJournal =
            new CommandSymbol(nameof(ShowClusterJournal),
                "show cluster journal",
                JournalResult);

        private static readonly string QueryResults =
            "(ClientActivityId: string, Text: string, Database: string, StartedOn: datetime, LastUpdatedOn: datetime, Duration: timespan, " +
            "State: string, RootActivityId: guid, User: string, FailureReason: string, TotalCpu: timespan, CacheStatistics: dynamic, " +
            "Application: string, MemoryPeak: long, ScannedEventStatistics: dynamic, Pricipal: string, ClientRequestProperties: dynamic, ResultSetStatistics: dynamic, WorkloadGroup: string)";

        public static readonly CommandSymbol ShowQueries =
            new CommandSymbol(nameof(ShowQueries),
                "show queries",
                "(ClientActivityId: string, Text: string, Database: string, StartedOn: datetime, LastUpdatedOn: datetime, Duration: timespan, State: string, RootActivityId: guid, User: string, FailureReason: string, TotalCpu: timespan, CacheStatistics: dynamic, Application: string, MemoryPeak: long, ScannedExtentsStatistics: dynamic, Principal: string, ClientRequestProperties: dynamic, ResultSetStatistics: dynamic, WorkloadGroup: string)");

        public static readonly CommandSymbol ShowRunningQueries =
            new CommandSymbol(nameof(ShowRunningQueries),
                "show running queries [by (user UserName=<string> | '*')]",
                QueryResults);

        public static readonly CommandSymbol CancelQuery =
            new CommandSymbol(nameof(CancelQuery),
                "cancel query ClientRequestId=<string>",
                UnknownResult);

        private static readonly string ShowQueryPlanPropertyList =
            PropertyList("reconstructCsl"
                );

        public static readonly CommandSymbol ShowQueryPlan =
            new CommandSymbol(nameof(ShowQueryPlan),
                $"show queryplan [{ShowQueryPlanPropertyList}] '<|' Query=<input_query>",
                "(ResultType: string, Format: string, Content: string)");

        public static readonly CommandSymbol ShowBasicAuthUsers =
            new CommandSymbol(nameof(ShowBasicAuthUsers),
                "show basicauth users",
                "(UserName: string)");

        public static readonly CommandSymbol CreateBasicAuthUser =
            new CommandSymbol(nameof(CreateBasicAuthUser),
                "create basicauth user UserName=<string> [password Password=<string>]",
                UnknownResult);

        public static readonly CommandSymbol DropBasicAuthUser =
            new CommandSymbol(nameof(DropBasicAuthUser),
                "drop basicauth user UserName=<string>",
                UnknownResult);

        public static readonly CommandSymbol ShowCache =
            new CommandSymbol(nameof(ShowCache),
                "show cache",
                "(NodeId: string, TotalMemoryCapacity: long, MemoryCacheCapacity: long, MemoryCacheInUse: long, MemoryCacheHitCount: long, " +
                "TotalDiskCapacity: long, DiskCacheCapacity: long, DiskCacheInUse: long, DiskCacheHitCount: long, DiskCacheMissCount: long, " +
                "MemoryCacheDetails: string, DiskCacheDetails: string)");

        public static readonly CommandSymbol AlterCache =
            new CommandSymbol(nameof(AlterCache),
                "alter cache on ('*' | NodeList=<bracketed_string>) Action=<bracketed_string>",
                UnknownResult);

        public static readonly CommandSymbol ShowCommands =
            new CommandSymbol(nameof(ShowCommands),
                "show commands",
                "(ClientActivityId: string, CommandType: string, Text: string, Database: string, " +
                "StartedOn: datetime, LastUpdatedOn: datetime, Duration: timespan, State: string, RootActivityId: guid, " +
                "User: string, FailureReason: string, Application: string, Principal: string, TotalCpu: timespan, " +
                "ResourcesUtilization: dynamic, ClientRequestProperties: dynamic, WorkloadGroup: string)");

        public static readonly CommandSymbol ShowCommandsAndQueries =
            new CommandSymbol(nameof(ShowCommandsAndQueries),
                "show commands-and-queries",
                "(ClientActivityId: string, CommandType: string, Text: string, Database: string, " +
                "StartedOn: datetime, LastUpdatedOn: datetime, Duration: timespan, State: string, FailureReason: string, RootActivityId: guid, " +
                "User: string, Application: string, Principal: string, ClientRequestProperties: dynamic, " +
                "TotalCpu: timespan, MemoryPeak: long, CacheStatistics: dynamic, ScannedExtentsStatistics: dynamic, ResultSetStatistics: dynamic, WorkloadGroup: string)");

        public static readonly CommandSymbol ShowIngestionFailures =
            new CommandSymbol(nameof(ShowIngestionFailures),
                "show ingestion failures [with '(' OperationId '=' OperationId=<guid> ')']",
                "(OperationId: guid, Database: string, Table: string, FailedOn: datetime, IngestionSourcePath: string, Details: string, FailureKind: string, RootActivityId: guid, OperationKind: string, OriginatesFromUpdatePolicy: bool, ErrorCode: string, Principal: string, ShouldRetry: bool, User: string, IngestionProperties: string)");
        #endregion

        #region Advanced Commands
        private static readonly string ShowExtentsResult = "(ExtentId: guid, DatabaseName: string, TableName: string, MaxCreatedOn: datetime, OriginalSize: real, ExtentSize: real, CompressedSize: real, IndexSize: real, Blocks: long, Segments: long, ExtentContainerId: string, RowCount: long, MinCreatedOn: datetime, Tags: string, Kind: string, DeletedRowCount: long)";
        private static readonly string ShowExtentsMetadataResult = "(ExtentId: guid, DatabaseName: string, TableName: string, ExtentMetadata: string)";

        private static readonly string TagWhereClause = "where { tags (has | contains | '!has' | '!contains')! Tag=<string>, and }+";
        private static readonly string WithFilteringPolicyClause = "with '(' extentsShowFilteringRuntimePolicy '=' policy=<value> ')' ";
        private static readonly string ShowExtentsSuffix = $"[{TagWhereClause}] [{WithFilteringPolicyClause}]";

        #region Cluster extents
        public static readonly CommandSymbol ShowClusterExtents =
            new CommandSymbol(nameof(ShowClusterExtents),
                $"show cluster extents [{ExtentIdList}] [hot] {ShowExtentsSuffix}",
                ShowExtentsResult);

        public static readonly CommandSymbol ShowClusterExtentsMetadata =
            new CommandSymbol(nameof(ShowClusterExtentsMetadata),
                $"show cluster extents [{ExtentIdList}] [hot] metadata {ShowExtentsSuffix}",
                ShowExtentsMetadataResult);
        #endregion

        #region Database extents
        public static readonly CommandSymbol ShowDatabaseExtents =
            new CommandSymbol(nameof(ShowDatabaseExtents),
                $"show (database [DatabaseName=<database>] | databases {DatabasesNameList}) extents [{ExtentIdList}] [hot] {ShowExtentsSuffix}",
                ShowExtentsResult);

        public static readonly CommandSymbol ShowDatabaseExtentsMetadata =
            new CommandSymbol(nameof(ShowDatabaseExtentsMetadata),
                $"show (database [DatabaseName=<database>] | databases {DatabasesNameList}) extents [{ExtentIdList}] [hot] metadata {ShowExtentsSuffix}",
                ShowExtentsMetadataResult);

        public static readonly CommandSymbol ShowDatabaseExtentTagsStatistics =
            new CommandSymbol(nameof(ShowDatabaseExtentTagsStatistics),
                $"show database extent tags statistics [with '(' minCreationTime '=' minCreationTime=<value> ')']",
                "(TableName: string, TotalExtentsCount: long, TaggedExtentsCount: long, TotalTagsCount: long, TotalTagsLength: long, DropByTagsCount: long, DropByTagsLength: long, IngestByTagsCount: long, IngestByTagsLength: long)");
        #endregion

        #region Table extents
        public static readonly CommandSymbol ShowTableExtents =
            new CommandSymbol(nameof(ShowTableExtents),
                $"show (table TableName=<table> | tables {TableNameList}) extents [{ExtentIdList}] [hot] {ShowExtentsSuffix}",
                ShowExtentsResult);

        public static readonly CommandSymbol ShowTableExtentsMetadata =
            new CommandSymbol(nameof(ShowTableExtentsMetadata),
                $"show (table TableName=<table> | tables {TableNameList}) extents [{ExtentIdList}] [hot] metadata {ShowExtentsSuffix}",
                ShowExtentsMetadataResult);
        #endregion

        #region class Shard groups
        private static readonly string ShardGroupsShowResult = "(Id: guid, ShardCount: long, RowCount: long, OriginalSize: long, ShardSize: long, CompressedSize: long, IndexSize: long, DeletedRowCount: long, V2ShardCount: long, V2RowCount: long, DateTimeColumnRanges: dynamic, Partition: dynamic)";
        private static readonly string ShardGroupsStatisticsShowResult = "(TableId: guid, long ShardGroupCount: long, ShardCount: long, RowCount: long, OriginalSize: long, ShardSize: long, CompressedSize: long, IndexSize: long, DeletedRowCount: long, V2ShardCount: long, V2RowCount: long)";

        public static readonly CommandSymbol TableShardGroupsShow =
            new CommandSymbol(nameof(TableShardGroupsShow),
                "show table TableName=<table> #shard-groups",
                ShardGroupsShowResult);

        public static readonly CommandSymbol TableShardGroupsStatisticsShow =
            new CommandSymbol(nameof(TableShardGroupsStatisticsShow),
                $"show (table TableName=<table> | tables {TableNameList}) #shard-groups #statistics",
                ShardGroupsStatisticsShowResult);

        public static readonly CommandSymbol DatabaseShardGroupsStatisticsShow =
            new CommandSymbol(nameof(DatabaseShardGroupsStatisticsShow),
                "show database [DatabaseName=<database>] #shard-groups #statistics",
                ShardGroupsStatisticsShowResult);
        #endregion

        private static readonly string MergeExtentsResult =
            "(OriginalExtentId: string, ResultExtentId: string, Duration: timespan)";

        private static readonly string GuidList = "'(' {GUID=<guid>, ','}+ ')'";
        public static readonly CommandSymbol MergeExtents =
            new CommandSymbol(nameof(MergeExtents),
                $"merge [async] TableName=<table> {GuidList} [with '(' rebuild '=' true ')']",
                MergeExtentsResult);

        public static readonly CommandSymbol MergeExtentsDryrun =
            new CommandSymbol(nameof(MergeExtentsDryrun),
                $"merge dryrun TableName=<table> {GuidList}",
                MergeExtentsResult);

        private static readonly string MoveExtentsResult =
            "(OriginalExtentId: string, ResultExtentId: string, Details: string)";

        public static readonly CommandSymbol MoveExtentsFrom =
            new CommandSymbol(nameof(MoveExtentsFrom),
                $"move [async] extents (all | {GuidList}) from table SourceTableName=<table> to table DestinationTableName=<table>",
                MoveExtentsResult);

        public static readonly CommandSymbol MoveExtentsQuery =
            new CommandSymbol(nameof(MoveExtentsQuery),
                $"move [async] extents to table DestinationTableName=<table> '<|' Query=<input_query>",
                MoveExtentsResult);

        public static readonly CommandSymbol ReplaceExtents =
            new CommandSymbol(nameof(ReplaceExtents),
                $"replace [async] extents in table DestinationTableName=<table> '<|' '{{' ExtentsToDropQuery=<input_query> '}}' ',' '{{' ExtentsToMoveQuery=<input_query> '}}'",
                MoveExtentsResult);

        private static readonly string DropExtentResult =
            "(ExtentId: guid, TableName: string, CreatedOn: datetime)";

        //public static readonly CommandSymbol DropExtentsQuery =
        //    new CommandSymbol("drop extents query", "drop extents [whatif] '<|' Query=<input_query>", DropExtentResult);

        public static readonly CommandSymbol DropExtent =
            new CommandSymbol(nameof(DropExtent),
                "drop extent ExtentId=<guid> [from TableName=<table>]",
                DropExtentResult);

        private static readonly string DropProperties = "[older Older=<long> (days | hours)] from (all tables! | TableName=<table>) [trim by! (extentsize | datasize) TrimSize=<long> (MB | GB | bytes)] [limit LimitCount=<long>]";

        public static readonly CommandSymbol DropExtents =
            new CommandSymbol(nameof(DropExtents),
                @"drop extents 
                    ('(' { ExtentId=<guid>, ',' }+ ')' [from TableName=<table>]
                     | whatif '<|'! Query=<input_query>
                     | '<|' Query=<input_query>
                     | older Older=<long> (days | hours) from (all tables! | TableName=<table>) [trim by! (extentsize | datasize)! TrimSize=<long> (MB | GB | bytes)] [limit LimitCount=<long>]
                     | from (all tables! | TableName=<table>) [trim by! (extentsize | datasize) TrimSize=<long> (MB | GB | bytes)] [limit LimitCount=<long>]
                     )",
                DropExtentResult);

        public static readonly CommandSymbol DropExtentsPartitionMetadata =
            new CommandSymbol(nameof(DropExtentsPartitionMetadata),
                "drop extents partition metadata csl=('<|' <input_query>)",
                UnknownResult);

        //public static readonly CommandSymbol DropAsyncExtentsPartitionMetadata =
        //    new CommandSymbol(nameof(DropAsyncExtentsPartitionMetadata),
        //        "drop async extents partition metadata csl=('<|' <input_query>)",
        //        UnknownResult);

        public static readonly CommandSymbol DropPretendExtentsByProperties =
            new CommandSymbol(nameof(DropPretendExtentsByProperties),
                $"drop-pretend extents {DropProperties}",
                DropExtentResult);

        public static readonly CommandSymbol ShowVersion =
            new CommandSymbol(nameof(ShowVersion),
                "show version",
                "(BuildVersion: string, BuildTime: datetime, ServiceType: string, ProductVersion: string)");

        public static readonly CommandSymbol ClearTableData =
           new CommandSymbol(nameof(ClearTableData),
               "clear [async] table TableName=<table> data",
               "(Status: string)");

        public static readonly CommandSymbol ClearTableCacheStreamingIngestionSchema =
           new CommandSymbol(nameof(ClearTableCacheStreamingIngestionSchema),
               "clear table TableName=<table> cache streamingingestion schema",
               "(NodeId: string, Status: string)");

        #endregion

        #region StoredQueryResults

        public static readonly CommandSymbol StoredQueryResultSet =
            new CommandSymbol(nameof(StoredQueryResultSet),
                $"set [async] stored_query_result StoredQueryResultName=<name> [{DataIngestionPropertyList}] '<|' Query=<input_query>",
                UnknownResult);

        private static string StoredQueryResultsShowResult =
            "(StoredQueryResultId:guid, Name:string, DatabaseName:string, PrincipalIdentity:string, SizeInBytes:long, RowCount:long, CreatedOn:datetime, ExpiresOn:datetime)";

        public static readonly CommandSymbol StoredQueryResultsShow =
            new CommandSymbol(nameof(StoredQueryResultsShow),
                "show stored_query_results [with '('! { PropertyName=<name> '='! Value=<value>, ',' } ')'!]",
                StoredQueryResultsShowResult);

        public static readonly CommandSymbol StoredQueryResultShowSchema =
            new CommandSymbol(nameof(StoredQueryResultShowSchema),
                "show stored_query_result StoredQueryResultName=<name> schema",
                "(StoredQueryResult:string, Schema:string)");

        public static readonly CommandSymbol StoredQueryResultDrop =
            new CommandSymbol(nameof(StoredQueryResultDrop),
                "drop stored_query_result StoredQueryResultName=<name>",
                StoredQueryResultsShowResult);

        public static readonly CommandSymbol StoredQueryResultsDrop =
            new CommandSymbol(nameof(StoredQueryResultsDrop),
                "drop stored_query_results by user Principal=<string>",
                StoredQueryResultsShowResult);

        #endregion

        public static readonly CommandSymbol CreateRequestSupport =
            new CommandSymbol(nameof(CreateRequestSupport),
                $"create request_support [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol ShowRequestSupport =
            new CommandSymbol(nameof(ShowRequestSupport),
                "show request_support key=<string>",
                UnknownResult);

        public static readonly CommandSymbol ShowClusterAdminState =
            new CommandSymbol(nameof(ShowClusterAdminState),
                "show cluster admin state",
                UnknownResult);

        public static readonly CommandSymbol ClearRemoteClusterDatabaseSchema =
            new CommandSymbol(nameof(ClearRemoteClusterDatabaseSchema),
                "clear cache remote-schema cluster '(' clusterName=<string> ')' '.' database '(' databaseName=<string> ')'",
                UnknownResult);

        public static readonly CommandSymbol ShowClusterMonitoring =
            new CommandSymbol(nameof(ShowClusterMonitoring),
                "show cluster monitoring",
                UnknownResult);

        public static readonly CommandSymbol ShowClusterScaleIn =
            new CommandSymbol(nameof(ShowClusterScaleIn),
                "show cluster scalein num=(<int> | <long>) nodes",
                UnknownResult);

        public static readonly CommandSymbol ShowClusterNetwork =
            new CommandSymbol(nameof(ShowClusterNetwork),
                "show cluster network [bytes=<long>]",
                UnknownResult);

        public static readonly CommandSymbol AlterClusterStorageKeys =
            new CommandSymbol(nameof(AlterClusterStorageKeys),
                $"alter [async] cluster storage keys [{PropertyList()}] decryption-certificate-thumbprint thumbprint=<string>",
                UnknownResult);

        public static readonly CommandSymbol ShowClusterStorageKeysHash =
            new CommandSymbol(nameof(ShowClusterStorageKeysHash),
            "show cluster storage keys hash",
            UnknownResult);


        // extent containers
        public static readonly CommandSymbol AlterExtentContainersAdd =
            new CommandSymbol(nameof(AlterExtentContainersAdd),
                "alter extentcontainers databaseName=<database> add container=<string> [hardDeletePeriod=<timespan> containerId=<guid>]",
                UnknownResult);

        public static readonly CommandSymbol AlterExtentContainersDrop =
            new CommandSymbol(nameof(AlterExtentContainersDrop),
                "alter extentcontainers databaseName=<database> drop [container=<guid>]",
                UnknownResult);

        public static readonly CommandSymbol AlterExtentContainersRecycle =
            new CommandSymbol(nameof(AlterExtentContainersRecycle),
                "alter extentcontainers databaseName=<database> recycle (container=<guid> | older hours=(<int> | <long>) hours)",
                UnknownResult);

        public static readonly CommandSymbol AlterExtentContainersSet =
            new CommandSymbol(nameof(AlterExtentContainersSet),
                "alter extentcontainers databaseName=<database> set state container=<guid> to (readonly | readwrite)",
                UnknownResult);

        public static readonly CommandSymbol ShowExtentContainers =
            new CommandSymbol(nameof(ShowExtentContainers),
                $"show extentcontainers [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol DropEmptyExtentContainers =
            new CommandSymbol(nameof(DropEmptyExtentContainers),
                $"drop [async] empty extentcontainers databaseName=<database> until '=' d=<datetime> [whatif] [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol CleanDatabaseExtentContainers =
            new CommandSymbol(nameof(CleanDatabaseExtentContainers),
                "clean databases [async] ['(' {databaseName=<database>, ','} ')'] extentcontainers",
                UnknownResult);

        public static readonly CommandSymbol ShowDatabaseExtentContainersCleanOperations =
            new CommandSymbol(nameof(ShowDatabaseExtentContainersCleanOperations),
                "show database databaseName=<database> extentcontainers clean operations [obj=<guid>]",
                UnknownResult);

        public static readonly CommandSymbol ClearDatabaseCacheQueryResults =
            new CommandSymbol(nameof(ClearDatabaseCacheQueryResults),
                "clear database cache query_results",
                UnknownResult);

        public static readonly CommandSymbol ShowDatabaseCacheQueryResults =
            new CommandSymbol(nameof(ShowDatabaseCacheQueryResults),
                "show database cache query_results",
                UnknownResult);

        public static readonly CommandSymbol ShowDatabasesManagementGroups =
            new CommandSymbol(nameof(ShowDatabasesManagementGroups),
                "show databases management groups",
                UnknownResult);

        //public static readonly CommandSymbol ShowDatabasesPolicies =
        //    new CommandSymbol(nameof(ShowDatabasesPolicies),
        //        "show databases '(' {DatabaseName=<database>, ','} ')' policies '(' {policyName=<name>, ','} ')'",
        //        UnknownResult);

        //public static readonly CommandSymbol ShowDatabasesPrincipals =
        //    new CommandSymbol(nameof(ShowDatabasesPrincipals),
        //        $"show databases '(' {{DatabaseName=<database>, ','}} ')' principals [from tenants '(' {{tenant=<string>, ','}} ')'] [{PropertyList()}]",
        //        UnknownResult);

        public static readonly CommandSymbol AlterDatabaseStorageKeys =
            new CommandSymbol(nameof(AlterDatabaseStorageKeys),
                $"alter [async] database databaseName=<database> storage keys [{PropertyList()}] decryption-certificate-thumbprint thumbprint=<string>",
                UnknownResult);

        public static readonly CommandSymbol ClearDatabaseCacheStreamingIngestionSchema =
            new CommandSymbol(nameof(ClearDatabaseCacheStreamingIngestionSchema),
                "clear database cache streamingingestion schema",
                UnknownResult);

        public static readonly CommandSymbol ClearDatabaseCacheQueryWeakConsistency =
            new CommandSymbol(nameof(ClearDatabaseCacheQueryWeakConsistency),
                "clear database cache query_weak_consistency",
                UnknownResult);

        public static readonly CommandSymbol ShowEntitySchema =
            new CommandSymbol(nameof(ShowEntitySchema),
                $"show entity entity=<name> schema as json [in databases '(' {{item=<string>, ','}}+ ')'] [except excludedFunctions=<string>] [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol ShowExtentDetails =
            new CommandSymbol(nameof(ShowExtentDetails),
                "show extent [details] (eid=<guid> | tname=<name>)",
                UnknownResult);

        public static readonly CommandSymbol ShowExtentColumnStorageStats =
            new CommandSymbol(nameof(ShowExtentColumnStorageStats),
                "show extent extentId=<guid> column columnName=<name> storage stats",
                UnknownResult);

        public static readonly CommandSymbol AttachExtentsIntoTableByContainer =
            new CommandSymbol(nameof(AttachExtentsIntoTableByContainer),
                "attach extents into table tableName=<table> by container containerUri=<string> {eid=<guid>}+",
                UnknownResult);

        public static readonly CommandSymbol AttachExtentsIntoTableByMetadata =
            new CommandSymbol(nameof(AttachExtentsIntoTableByMetadata),
                "attach [async] extents {into table tableName=<table>} by metadata csl=('<|' <input_query>)",
                UnknownResult);

        public static readonly CommandSymbol AlterExtentTagsFromQuery =
            new CommandSymbol(nameof(AlterExtentTagsFromQuery),
                "alter [async] extent tags '(' {t=<string>, ','}+ ')' csl=('<|' <input_query>)",
                UnknownResult);

        //public static readonly CommandSymbol DropExtentTagsFromQuery =
        //    new CommandSymbol(nameof(DropExtentTagsFromQuery),
        //        "drop [async] extent tags csl=('<|' <input_query>)",
        //        UnknownResult);

        //public static readonly CommandSymbol DropExtentTagsFromTable =
        //    new CommandSymbol(nameof(DropExtentTagsFromTable),
        //        "drop [async] extent tags from table tableName=<table> '(' {t=<string>, ','}+ ')'",
        //        UnknownResult);

        public static readonly CommandSymbol DropExtentTagsRetention =
            new CommandSymbol(nameof(DropExtentTagsRetention),
                "drop extent tags retention",
                UnknownResult);

        public static readonly CommandSymbol AlterFollowerClusterConfiguration =
            new CommandSymbol(nameof(AlterFollowerClusterConfiguration),
                "alter follower cluster configuration from leaderClusterMetadataPath=<string> (follow-authorized-principals '=' followAuthorizedPrincipals=<bool> | default-principals-modification-kind '=' modificationKind=(none | union | replace) | default-caching-policies-modification-kind '=' modificationKind=(none | union | replace))",
                UnknownResult);

        public static readonly CommandSymbol AddFollowerDatabaseAuthorizedPrincipals =
            new CommandSymbol(nameof(AddFollowerDatabaseAuthorizedPrincipals),
                "add follower database dbName=<database> [from leaderClusterMetadataPath=<string>] operationRole=(admins | users | viewers | unrestrictedviewers | monitors) '(' {principal=<string>, ','}+ ')' [notes=<string>]",
                UnknownResult);

        public static readonly CommandSymbol DropFollowerDatabaseAuthorizedPrincipals =
            new CommandSymbol(nameof(DropFollowerDatabaseAuthorizedPrincipals),
                "drop follower database dbName=<database> operationRole=(admins | users | viewers | unrestrictedviewers | monitors) [from leaderClusterMetadataPath=<string>] '(' {principal=<string>, ','}+ ')'",
                UnknownResult);

        public static readonly CommandSymbol AlterFollowerDatabaseAuthorizedPrincipals =
            new CommandSymbol(nameof(AlterFollowerDatabaseAuthorizedPrincipals),
                "alter follower database dbName=<database> [from leaderClusterMetadataPath=<string>] policy caching (hotdata '=' hotDataToken=<timespan> hotindex '=' hotIndexToken=<timespan> | hot '=' hotToken=<timespan>) hotWindows=[[','] {hot_window '=' p=(d1=<datetime> '..' d2=<datetime>), ','}+]",
                UnknownResult);

        public static readonly CommandSymbol DropFollowerDatabasePolicyCaching =
            new CommandSymbol(nameof(DropFollowerDatabasePolicyCaching),
                "delete follower database dbName=<database> policy caching",
                UnknownResult);

        public static readonly CommandSymbol AlterFollowerDatabaseChildEntities =
            new CommandSymbol(nameof(AlterFollowerDatabaseChildEntities),
                "alter follower database dbName=<database> [from leaderClusterMetadataPath=<string>] (tables | external tables | materialized-views) entityListKind=(exclude | include) operationName=(add | drop) '(' {ename=<wildcarded_name>, ','}+ ')'",
                UnknownResult);

        public static readonly CommandSymbol AlterFollowerDatabaseConfiguration =
            new CommandSymbol(nameof(AlterFollowerDatabaseConfiguration),
                "alter follower database dbName=<database> [from leaderClusterMetadataPath=<string>] (principals-modification-kind '=' modificationKind=(none | union | replace) | caching-policies-modification-kind '=' modificationKind=(none | union | replace) | prefetch-extents '=' prefetchExtents=<bool> | metadata serializedDatabaseMetadataOverride=<string>)",
                UnknownResult);

        //public static readonly CommandSymbol AddFollowerDatabase =
        //    new CommandSymbol(nameof(AddFollowerDatabase),
        //        "add follower database databaseName=<database> from leaderClusterMetadataPath=<string> metadata serializedDatabaseMetadataOverride=<string> | add follower (database databaseName=<wildcarded_name> [tables [exclude '(' {tableName=<wildcarded_name>, ','}+ ')'] [include '(' {tableName=<wildcarded_name>, ','}+ ')']] [external tables [exclude '(' {externalTableName=<wildcarded_name>, ','}+ ')'] [include '(' {externalTableName=<wildcarded_name>, ','}+ ')']] [materialized-views [exclude '(' {materializedViewName=<wildcarded_name>, ','}+ ')'] [include '(' {materializedViewName=<wildcarded_name>, ','}+ ')']] | databases '(' {databaseName=<database>, ','}+ ')') from leaderClusterMetadataPath=<string> [default-principals-modification-kind '=' modificationKind=(none | union | replace)] [default-caching-policies-modification-kind '=' modificationKind=(none | union | replace)]",
        //        UnknownResult);

        public static readonly CommandSymbol DropFollowerDatabases =
            new CommandSymbol(nameof(DropFollowerDatabases),
                "drop follower (database databaseName=<database> | databases '(' {databaseName=<database>, ','}+ ')') from leaderClusterMetadataPath=<string>",
                UnknownResult);

        public static readonly CommandSymbol ShowFollowerDatabase =
            new CommandSymbol(nameof(ShowFollowerDatabase),
                "show follower (database databaseName=<database> | databases ['(' {databaseName=<database>, ','} ')'])",
                UnknownResult);

        public static readonly CommandSymbol AlterFollowerTablesPolicyCaching =
            new CommandSymbol(nameof(AlterFollowerTablesPolicyCaching),
                "alter follower database dbName=<database> [from leaderClusterMetadataPath=<string>] (table name=<table> | materialized-view name=<materializedview> | tables '(' {name=<name>, ','}+ ')' | materialized-views '(' {name=<name>, ','}+ ')') policy caching (hotdata '=' hotDataToken=<timespan> hotindex '=' hotIndexToken=<timespan> | hot '=' hotToken=<timespan>) hotWindows=[[','] {hot_window '=' p=(d1=<datetime> '..' d2=<datetime>), ','}+]",
                UnknownResult);

        public static readonly CommandSymbol DropFollowerTablesPolicyCaching =
            new CommandSymbol(nameof(DropFollowerTablesPolicyCaching),
                "delete follower database dbName=<database> (table name=<table> | materialized-view name=<materializedview> | tables '(' {name=<name>, ','}+ ')' | materialized-views '(' {name=<name>, ','}+ ')') policy caching",
                UnknownResult);

        public static readonly CommandSymbol ShowFreshness =
            new CommandSymbol(nameof(ShowFreshness),
                "show #freshness tableName=<table> [column columnName=<column>] [threshold threshold=<long>]",
                UnknownResult);

        public static readonly CommandSymbol ShowFunctionSchemaAsJson =
            new CommandSymbol(nameof(ShowFunctionSchemaAsJson),
                "show function functionName=<function> schema as json",
                UnknownResult);

        public static readonly CommandSymbol SetMaterializedViewAdmins =
            new CommandSymbol(nameof(SetMaterializedViewAdmins),
                "set materialized-view materializedViewName=<materializedview> admins (none | '(' {principal=<string>, ','}+ ')' [notes=<string>])",
                UnknownResult);

        public static readonly CommandSymbol AddMaterializedViewAdmins =
            new CommandSymbol(nameof(AddMaterializedViewAdmins),
                "add materialized-view materializedViewName=<materializedview> admins '(' {principal=<string>, ','}+ ')' [notes=<string>]",
                UnknownResult);

        public static readonly CommandSymbol DropMaterializedViewAdmins =
            new CommandSymbol(nameof(DropMaterializedViewAdmins),
                "drop materialized-view materializedViewName=<materializedview> admins '(' {principal=<string>, ','}+ ')' [notes=<string>]",
                UnknownResult);

        public static readonly CommandSymbol SetMaterializedViewConcurrency =
            new CommandSymbol(nameof(SetMaterializedViewConcurrency),
                "set materialized-view viewName=<materializedview> concurrency ['=' n=(<int> | <long>)]",
                UnknownResult);

        public static readonly CommandSymbol ClearMaterializedViewStatistics =
            new CommandSymbol(nameof(ClearMaterializedViewStatistics),
                "clear materialized-view viewName=<materializedview> statistics",
                UnknownResult);

        public static readonly CommandSymbol ShowMaterializedViewStatistics =
            new CommandSymbol(nameof(ShowMaterializedViewStatistics),
                "show materialized-view viewName=<materializedview> statistics",
                UnknownResult);

        public static readonly CommandSymbol ShowMaterializedViewDiagnostics =
            new CommandSymbol(nameof(ShowMaterializedViewDiagnostics),
                $"show materialized-view viewName=<materializedview> diagnostics [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol ShowMaterializedViewFailures =
            new CommandSymbol(nameof(ShowMaterializedViewFailures),
                "show materialized-view viewName=<materializedview> failures",
                UnknownResult);

        public static readonly CommandSymbol ShowMemory =
            new CommandSymbol(nameof(ShowMemory),
                "show memory [details]",
                UnknownResult);

        public static readonly CommandSymbol CancelOperation =
            new CommandSymbol(nameof(CancelOperation),
                $"cancel operation obj=<guid> [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol DisablePlugin =
            new CommandSymbol(nameof(DisablePlugin),
                "disable plugin pluginName=(<string> | <name>)",
                UnknownResult);

        public static readonly CommandSymbol EnablePlugin =
            new CommandSymbol(nameof(EnablePlugin),
                "enable plugin name=(<string> | <name>)",
                UnknownResult);

        public static readonly CommandSymbol ShowPlugins =
            new CommandSymbol(nameof(ShowPlugins),
                $"show plugins [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol ShowPrincipalAccess =
            new CommandSymbol(nameof(ShowPrincipalAccess),
                $"show principal access [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol ShowDatabasePurgeOperation =
            new CommandSymbol(nameof(ShowDatabasePurgeOperation),
                "show database databaseName=<database> purge (operation obj=<guid> | operations [obj=<guid>])",
                UnknownResult);

        public static readonly CommandSymbol ShowQueryExecution =
            new CommandSymbol(nameof(ShowQueryExecution),
                "show queryexecution queryText=('<|' <input_query>)",
                UnknownResult);

        public static readonly CommandSymbol AlterPoliciesOfRetention =
            new CommandSymbol(nameof(AlterPoliciesOfRetention),
                "alter policies of retention [internal] policies=<string>",
                UnknownResult);

        public static readonly CommandSymbol DeletePoliciesOfRetention =
            new CommandSymbol(nameof(DeletePoliciesOfRetention),
                "delete policies of retention '(' {entity=<string>, ','}+ ')'",
                UnknownResult);

        //public static readonly CommandSymbol AttachRowStore =
        //    new CommandSymbol(nameof(AttachRowStore),
        //        $"attach rowstore rowStoreName=<name> rowStoreId=<guid> writeaheadlog waLogPath=<string> [{PropertyList()}]",
        //        UnknownResult);

        public static readonly CommandSymbol CreateRowStore =
            new CommandSymbol(nameof(CreateRowStore),
                $"create rowstore [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol DropRowStore =
            new CommandSymbol(nameof(DropRowStore),
                "(drop | detach) rowstore rowStoreName=<name> [ifexists]",
                UnknownResult);

        public static readonly CommandSymbol ShowRowStore =
            new CommandSymbol(nameof(ShowRowStore),
                "show rowstore rowStoreName=<name>",
                UnknownResult);

        public static readonly CommandSymbol ShowRowStores =
            new CommandSymbol(nameof(ShowRowStores),
                "show rowstores",
                UnknownResult);

        public static readonly CommandSymbol ShowRowStoreTransactions =
            new CommandSymbol(nameof(ShowRowStoreTransactions),
                "show rowstore transactions",
                UnknownResult);

        public static readonly CommandSymbol ShowRowStoreSeals =
            new CommandSymbol(nameof(ShowRowStoreSeals),
                $"show rowstore seals tableName=<string> [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol ShowSchema =
            new CommandSymbol(nameof(ShowSchema),
                "show [cluster] schema [details | as json]",
                UnknownResult);

        public static readonly CommandSymbol ShowCallStacks =
            new CommandSymbol(nameof(ShowCallStacks),
                $"show callstacks [for duration=<timespan>] [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol ShowStreamingIngestionFailures =
            new CommandSymbol(nameof(ShowStreamingIngestionFailures),
                "show streamingingestion failures",
                UnknownResult);

        public static readonly CommandSymbol ShowStreamingIngestionStatistics =
            new CommandSymbol(nameof(ShowStreamingIngestionStatistics),
                "show streamingingestion statistics",
                UnknownResult);

        public static readonly CommandSymbol AlterTableRowStoreReferencesDropKey =
            new CommandSymbol(nameof(AlterTableRowStoreReferencesDropKey),
                $@"alter table TableName=<database_table> rowstore_references drop key rowStoreKey=<string> [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol AlterTableRowStoreReferencesDropRowStore =
            new CommandSymbol(nameof(AlterTableRowStoreReferencesDropRowStore),
                $@"alter table TableName=<database_table> rowstore_references drop rowstore rowStoreName=<name> [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol AlterTableRowStoreReferencesDropBlockedKeys =
            new CommandSymbol(nameof(AlterTableRowStoreReferencesDropBlockedKeys),
                $@"alter table TableName=<database_table> rowstore_references drop blocked keys [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol AlterTableRowStoreReferencesDisableKey =
            new CommandSymbol(nameof(AlterTableRowStoreReferencesDisableKey),
                $@"alter table TableName=<database_table> rowstore_references disable key rowStoreKey=<string> [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol AlterTableRowStoreReferencesDisableRowStore =
            new CommandSymbol(nameof(AlterTableRowStoreReferencesDisableRowStore),
                $@"alter table TableName=<database_table> rowstore_references disable rowstore rowStoreName=<name> [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol AlterTableRowStoreReferencesDisableBlockedKeys =
            new CommandSymbol(nameof(AlterTableRowStoreReferencesDisableBlockedKeys),
                $@"alter table TableName=<database_table> rowstore_references disable blocked keys [{PropertyList()}]",
                UnknownResult);

        public static readonly CommandSymbol SetTableRowStoreReferences =
            new CommandSymbol(nameof(SetTableRowStoreReferences),
                "set table TableName=<database_table> rowstore_references references=<string>",
                UnknownResult);

        public static readonly CommandSymbol ShowTableRowStoreReferences =
            new CommandSymbol(nameof(ShowTableRowStoreReferences),
                "show table TableName=<database_table> rowstore_references",
                UnknownResult);

        public static readonly CommandSymbol AlterTableColumnStatistics =
            new CommandSymbol(nameof(AlterTableColumnStatistics),
                "alter table TableName=<table> column statistics {c2=<name> statisticsValues2=<string>, ','}",
                UnknownResult);

        public static readonly CommandSymbol AlterTableColumnStatisticsMethod =
            new CommandSymbol(nameof(AlterTableColumnStatisticsMethod),
                "alter table TableName=<database_table> column statistics method '=' newMethod=<string>",
                UnknownResult);

        public static readonly CommandSymbol ShowTableColumnStatitics =
            new CommandSymbol(nameof(ShowTableColumnStatitics),
                "show table TableName=<table> column statistics",
                UnknownResult);

        public static readonly CommandSymbol ShowTableDimensions =
            new CommandSymbol(nameof(ShowTableDimensions),
                "show table TableName=<table> dimensions",
                UnknownResult);

        public static readonly CommandSymbol DeleteTableRecords =
            new CommandSymbol(nameof(DeleteTableRecords),
                $"delete [async] table TableName=<table> records [{PropertyList()}] csl=('<|' <input_query>)",
                UnknownResult);

        public static readonly CommandSymbol ShowTableColumnsClassification =
            new CommandSymbol(nameof(ShowTableColumnsClassification),
                "show table TableName=<table> columns classification",
                UnknownResult);

        public static readonly CommandSymbol ShowTableRowStores =
            new CommandSymbol(nameof(ShowTableRowStores),
                "show table tableName=<database_table> rowstores",
                UnknownResult);

        public static readonly CommandSymbol ShowTableRowStoreSealInfo =
            new CommandSymbol(nameof(ShowTableRowStoreSealInfo),
                "show table tableName=<database_table> rowstore_sealinfo",
                UnknownResult);

        //public static readonly CommandSymbol ShowDatabasesTablesPolicies =
        //    new CommandSymbol(nameof(ShowDatabasesTablesPolicies),
        //        "show databases '(' {DatabaseName=<database>, ','} ')' tables policies '(' {PolicyName=<name>, ','} ')'",
        //        UnknownResult);

        public static readonly CommandSymbol ShowTablesColumnStatistics =
            new CommandSymbol(nameof(ShowTablesColumnStatistics),
                "show tables column statistics older outdatewindow=<timespan>",
                UnknownResult);

        public static readonly CommandSymbol ShowTableUsageStatistics =
            new CommandSymbol(nameof(ShowTableUsageStatistics),
                "show table usage statistics [by partitionBy=<timespan>]",
                UnknownResult);

        public static readonly CommandSymbol ShowTableUsageStatisticsDetails =
            new CommandSymbol(nameof(ShowTableUsageStatisticsDetails),
                "show table usage statistics details",
                UnknownResult);

        public static readonly CommandSymbol CreateTempStorage =
            new CommandSymbol(nameof(CreateTempStorage),
                "create tempstorage",
                UnknownResult);

        public static readonly CommandSymbol DropTempStorage =
            new CommandSymbol(nameof(DropTempStorage),
                "drop tempstorage older olderThan=<timespan>",
                UnknownResult);

        public static readonly CommandSymbol DropStoredQueryResultContainers =
            new CommandSymbol(nameof(DropStoredQueryResultContainers),
                "drop storedqueryresultcontainers DatabaseName=<database> {containerId=<guid>}+",
                UnknownResult);

        public static readonly CommandSymbol DropUnusedStoredQueryResultContainers =
            new CommandSymbol(nameof(DropUnusedStoredQueryResultContainers),
                "drop unused storedqueryresultcontainers databaseName=<database>",
                UnknownResult);

        public static readonly CommandSymbol EnableDatabaseMaintenanceMode =
            new CommandSymbol(nameof(EnableDatabaseMaintenanceMode),
                "enable database DatabaseName=<database> maintenance_mode",
                UnknownResult);

        public static readonly CommandSymbol DisableDatabaseMaintenanceMode =
            new CommandSymbol(nameof(DisableDatabaseMaintenanceMode),
                "disable database DatabaseName=<database> maintenance_mode",
                UnknownResult);

        public static readonly CommandSymbol ShowQueryCallTree =
            new CommandSymbol(nameof(ShowQueryCallTree),
                "show query call-tree queryText=('<|' <input_query>)",
                UnknownResult);

        public static IReadOnlyList<CommandSymbol> All { get; } =
            new CommandSymbol[]
            {
                #region Schema Commands
                // Databases
                ShowDatabase,
                ShowDatabaseDetails,
                ShowDatabaseIdentity,
                ShowDatabasePolicies,
                ShowDatabaseDataStats,
                ShowClusterDatabases,
                ShowClusterDatabasesDetails,
                ShowClusterDatabasesIdentity,
                ShowClusterDatabasesPolicies,
                ShowClusterDatabasesDataStats,
                CreateDatabasePersist,
                CreateDatabaseVolatile,
                AttachDatabase,
                AttachDatabaseMetadata,
                DetachDatabase,
                AlterDatabasePrettyName,
                DropDatabasePrettyName,
                AlterDatabasePersistMetadata,
                SetAccess,
                ShowDatabaseSchema,
                ShowDatabaseSchemaViolations,
                ShowDatabaseSchemaAsJson,
                ShowDatabaseSchemaAsCslScript,
                ShowDatabaseCslSchema,
                ShowDatabasesSchema,
                ShowDatabasesSchemaAsJson,
                CreateDatabaseIngestionMapping,
                AlterDatabaseIngestionMapping,
                ShowDatabaseIngestionMappings,
                DropDatabaseIngestionMapping,

                // Tables
                ShowTables,
                ShowTable,
                ShowTablesDetails,
                ShowTableDetails,
                ShowTableCslSchema,
                ShowTableSchemaAsJson,
                CreateTable,
                CreateTableBasedOnAnother,
                CreateMergeTable,
                CreateTables,
                CreateMergeTables,
                DefineTables,
                AlterTable,
                AlterMergeTable,
                RenameTable,
                RenameTables,
                DropTable,
                UndoDropTable,
                DropTables,
                CreateTableIngestionMapping,
                AlterTableIngestionMapping,
                ShowTableIngestionMappings,
                ShowTableIngestionMapping,
                DropTableIngestionMapping,
                AlterTableDocString,
                AlterTableFolder,

                // Columns
                RenameColumn,
                RenameColumns,
                AlterColumnType,
                DropColumn,
                DropTableColumns,
                AlterTableColumnDocStrings,
                AlterMergeTableColumnDocStrings,

                // Functions
                ShowFunctions,
                ShowFunction,
                DropFunction,
                DropFunctions,
                AlterFunctionDocString,
                AlterFunctionFolder,
                CreateFunction,
                AlterFunction,
                CreateOrAlterFunction,

                // External Tables
                ShowExternalTables,
                ShowExternalTable,
                ShowExternalTableCslSchema,
                ShowExternalTableSchema,
                ShowExternalTableArtifacts,
                DropExternalTable,
                CreateStorageExternalTable,
                CreateSqlExternalTable,
                AlterStorageExternalTable,
                AlterSqlExternalTable,
                CreateOrAlterStorageExternalTable,
                CreateOrAlterSqlExternalTable,
                CreateExternalTableMapping,
                AlterExternalTableMapping,
                ShowExternalTableMapping,
                ShowExternalTableMappings,
                DropExternalTableMapping,

                // Workload groups
                ShowWorkloadGroup,
                ShowWorkloadGroups,
                CreateOrAleterWorkloadGroup,
                AlterMergeWorkloadGroup,
                DropWorkloadGroup,
                #endregion

                #region Policy Commands
                // Caching
                ShowDatabasePolicyCaching,
                ShowTablePolicyCaching,
                ShowTableStarPolicyCaching,
                ShowColumnPolicyCaching,
                ShowMaterializedViewPolicyCaching,
                ShowClusterPolicyCaching,

                AlterDatabasePolicyCaching,
                AlterTablePolicyCaching,
                AlterTablesPolicyCaching,
                AlterColumnPolicyCaching,
                AlterMaterializedViewPolicyCaching,
                AlterClusterPolicyCaching,

                DeleteDatabasePolicyCaching,
                DeleteTablePolicyCaching,
                DeleteColumnPolicyCaching,
                DeleteMaterializedViewPolicyCaching,
                DeleteClusterPolicyCaching,

                // IngestionTime
                AlterTablePolicyIngestionTime,
                AlterTablesPolicyIngestionTime,
                ShowTablePolicyIngestionTime,
                ShowTableStarPolicyIngestionTime,
                DeleteTablePolicyIngestionTime,

                // RowLevelSecurity
                ShowTablePolicyRowLevelSecurity,
                ShowTableStarPolicyRowLevelSecurity,
                AlterTablePolicyRowLevelSecurity,
                DeleteTablePolicyRowLevelSecurity,
                ShowMaterializedViewPolicyRowLevelSecurity,
                AlterMaterializedViewPolicyRowLevelSecurity,
                DeleteMaterializedViewPolicyRowLevelSecurity,

                // ManagedIdentity
                ShowDatabasePolicyManagedIdentity,
                ShowClusterPolicyManagedIdentity,
                AlterDatabasePolicyManagedIdentity,
                AlterClusterPolicyManagedIdentity,
                DeleteDatabasePolicyManagedIdentity,
                DeleteClusterPolicyManagedIdentity,

                // Retention
                ShowTablePolicyRetention,
                ShowTableStarPolicyRetention,
                ShowDatabasePolicyRetention,
                DeleteTablePolicyRetention,
                DeleteDatabasePolicyRetention,
                AlterTablePolicyRetention,
                AlterMaterializedViewPolicyRetention,
                AlterTablesPolicyRetention,
                AlterDatabasePolicyRetention,
                AlterMergeTablePolicyRetention,
                AlterMergeMaterializedViewPolicyRetention,
                AlterMergeDatabasePolicyRetention,
                ShowDatabasePolicyHardRetentionViolations,
                ShowDatabasePolicySoftRetentionViolations,

                // RowOrder
                ShowTablePolicyRowOrder,
                ShowTableStarPolicyRowOrder,
                DeleteTablePolicyRowOrder,
                AlterTablePolicyRowOrder,
                AlterTablesPolicyRowOrder,
                AlterMergeTablePolicyRowOrder,

                // Update
                ShowTablePolicyUpdate,
                ShowTableStarPolicyUpdate,
                AlterTablePolicyUpdate,
                AlterMergeTablePolicyUpdate,
                DeleteTablePolicyUpdate,

                // IngestionBatching
                ShowClusterPolicyIngestionBatching,
                ShowDatabasePolicyIngestionBatching,
                ShowTablePolicyIngestionBatching,
                ShowTableStarPolicyIngestionBatching,
                AlterClusterPolicyIngestionBatching,
                AlterDatabasePolicyIngestionBatching,
                AlterTablePolicyIngestionBatching,
                AlterTablesPolicyIngestionBatching,
                DeleteDatabasePolicyIngestionBatching,
                DeleteTablePolicyIngestionBatching,

                // Encoding
                ShowDatabasePolicyEncoding,
                ShowTablePolicyEncoding,
                ShowColumnPolicyEncoding,
                AlterDatabasePolicyEncoding,
                AlterTablePolicyEncoding,
                AlterTableColumnsPolicyEncoding,
                AlterColumnPolicyEncoding,
                AlterColumnPolicyEncodingType,
                AlterMergeDatabasePolicyEncoding,
                AlterMergeTablePolicyEncoding,
                AlterMergeColumnPolicyEncoding,
                DeleteDatabasePolicyEncoding,
                DeleteTablePolicyEncoding,
                DeleteColumnPolicyEncoding,

                // Merge
                ShowDatabasePolicyMerge,
                ShowTablePolicyMerge,
                ShowTableStarPolicyMerge,
                AlterDatabasePolicyMerge,
                AlterTablePolicyMerge,
                AlterTablesPolicyMerge,
                AlterMergeDatabasePolicyMerge,
                AlterMergeTablePolicyMerge,
                DeleteDatabasePolicyMerge,
                DeleteTablePolicyMerge,

                // Partitioning
                ShowTablePolicyPartitioning,
                ShowTableStarPolicyPartitioning,
                AlterTablePolicyPartitioning,
                AlterMergeTablePolicyPartitioning,
                DeleteTablePolicyPartitioning,

                // Request classification
                ShowClusterPolicyRequestClassification,
                AlterClusterPolicyRequestClassification,
                AlterMergeClusterPolicyRequestClassification,
                DeleteClusterPolicyRequestClassification,

                // Restricted View Access
                ShowTablePolicyRestrictedViewAccess,
                ShowTableStarPolicyRestrictedViewAccess,
                AlterTablePolicyRestrictedViewAccess,
                AlterTablesPolicyRestrictedViewAccess,
                DeleteTablePolicyRestrictedViewAccess,

                // Row Store
                ShowClusterPolicyRowStore,
                AlterClusterPolicyRowStore,
                AlterMergeClusterPolicyRowStore,
                DeleteClusterPolicyRowStore,

                // Auto Delete
                ShowTablePolicyAutoDelete,
                AlterTablePolicyAutoDelete,
                DeleteTablePolicyAutoDelete,

                // Sandbox
                ShowClusterPolicySandbox,
                AlterClusterPolicySandbox,
                DeleteClusterPolicySandbox,
                ShowClusterSandboxesStats,

                // Sharding
                ShowDatabasePolicySharding,
                ShowTablePolicySharding,
                ShowTableStarPolicySharding,
                AlterDatabasePolicySharding,
                AlterTablePolicySharding,
                AlterMergeDatabasePolicySharding,
                AlterMergeTablePolicySharding,
                DeleteDatabasePolicySharding,
                DeleteTablePolicySharding,
                AlterClusterPolicySharding,
                DeleteClusterPolicySharding,
                ShowClusterPolicySharding,

                // Streaming Ingestion
                ShowDatabasePolicyStreamingIngestion,
                ShowTablePolicyStreamingIngestion,
                ShowClusterPolicyStreamingIngestion,
                AlterDatabasePolicyStreamingIngestion,
                AlterMergeDatabasePolicyStreamingIngestion,
                AlterTablePolicyStreamingIngestion,
                AlterMergeTablePolicyStreamingIngestion,
                AlterClusterPolicyStreamingIngestion,
                AlterMergeClusterPolicyStreamingIngestion,
                DeleteDatabasePolicyStreamingIngestion,
                DeleteTablePolicyStreamingIngestion,
                DeleteClusterPolicyStreamingIngestion,

                // Callout
                ShowClusterPolicyCallout,
                AlterClusterPolicyCallout,
                AlterMergeClusterPolicyCallout,
                DeleteClusterPolicyCallout,

                // Capacity
                ShowClusterPolicyCapacity,
                AlterClusterPolicyCapacity,
                AlterMergeClusterPolicyCapacity,

                // Multi Database Admins
                ShowClusterPolicyMultiDatabaseAdmins,
                AlterClusterPolicyMultiDatabaseAdmins,
                AlterMergeClusterPolicyMultiDatabaseAdmins,

                // Query Weak Consistency
                ShowClusterPolicyQueryWeakConsistency,
                AlterClusterPolicyQueryWeakConsistency,
                AlterMergeClusterPolicyQueryWeakConsistency,

                // Diagnostics Settings
                ShowDatabasePolicyDiagnostics,
                ShowClusterPolicyDiagnostics,
                AlterDatabasePolicyDiagnostics,
                AlterClusterPolicyDiagnostics,
                DeleteDatabasePolicyDiagnostics,
                AlterMergeDatabasePolicyDiagnostics,
                AlterMergeClusterPolicyDiagnostics,
                
                // Extent tags retention
                ShowDatabasePolicyExtentTagsRetention,
                ShowTablePolicyExtentTagsRetention,
                ShowTableStarPolicyExtentTagsRetention,
                AlterDatabasePolicyExtentTagsRetention,
                AlterTablePolicyExtentTagsRetention,
                DeleteDatabasePolicyExtentTagsRetention,
                DeleteTablePolicyExtentTagsRetention,
                #endregion

                #region Security Role Commands
                ShowPrincipalRoles,
                ShowDatabasePrincipalRoles,
                ShowTablePrincipalRoles,
                ShowExternalTablesPrincipalRoles,
                ShowFunctionPrincipalRoles,
                ShowClusterPrincipalRoles,

                ShowClusterPrincipals,
                ShowDatabasePrincipals,
                ShowTablePrincipals,
                ShowFunctionPrincipals,

                AddClusterRole,
                DropClusterRole,
                SetClusterRole,

                AddDatabaseRole,
                DropDatabaseRole,
                SetDatabaseRole,

                AddTableRole,
                DropTableRole,
                SetTableRole,

                AddFunctionRole,
                DropFunctionRole,
                SetFunctionRole,

                ShowClusterBlockedPrincipals,
                AddClusterBlockedPrincipals,
                DropClusterBlockedPrincipals,
                #endregion

                #region Data Ingestion
                IngestInlineIntoTable,
                IngestIntoTable,
                SetTable,
                AppendTable,
                SetOrAppendTable,
                SetOrReplaceTable,
                #endregion

                #region Data Export
                ExportToStorage,
                ExportToSqlTable,
                ExportToExternalTable,
                CreateOrAlterContinuousExport,
                ShowContinuousExport,
                ShowContinuousExports,
                ShowClusterPendingContinuousExports,
                ShowContinuousExportExportedArtifacts,
                ShowContinuousExportFailures,
                SetContinuousExportCursor,
                DropContinuousExport,
                EnableContinuousExport,
                DisableContinuousExport,
                #endregion

                #region Materialized Views 
                AlterMaterializedViewDocString,
                AlterMaterializedViewFolder,
                AlterMaterializedViewAutoUpdateSchema,
                AlterMaterializedViewLookback,
                AlterMaterializedViewPolicyPartitioning,
                AlterMergeMaterializedViewPolicyPartitioning,
                CreateMaterializedView,
                CreateOrAlterMaterializedView,
                ShowMaterializedView,
                ShowMaterializedViews,
                ShowMaterializedViewDetails,
                ShowMaterializedViewsDetails,
                ShowMaterializedViewExtents,
                ShowMaterializedViewPolicyRetention,
                ShowMaterializedViewPolicyMerge,
                AlterMaterializedView,
                ClearMaterializedViewData,
                SetMaterializedViewCursor,
                DeleteMaterializedViewPolicyPartitioning,
                DropMaterializedView,
                EnableDisableMaterializedView,
                ShowMaterializedViewPrincipals,
                ShowMaterializedViewSchemaAsJson,
                ShowMaterializedViewCslSchema,
                ShowMaterializedViewPolicyPartitioning,
                RenameMaterializedView,
                #endregion 

                #region System Information Commands
                ShowCluster,
                ShowDiagnostics,
                ShowCapacity,

                ShowOperations,
                ShowOperationDetails,

                ShowJournal,
                ShowDatabaseJournal,
                ShowClusterJournal,

                ShowQueries,
                ShowRunningQueries,
                CancelQuery,
                ShowQueryPlan,

                ShowBasicAuthUsers,
                CreateBasicAuthUser,
                DropBasicAuthUser,

                ShowCache,
                AlterCache,

                ShowCommands,
                ShowCommandsAndQueries,

                ShowIngestionFailures,
                #endregion

                #region Advanced Commands
                ShowClusterExtents,
                ShowClusterExtentsMetadata,
                ShowDatabaseExtents,
                ShowDatabaseExtentsMetadata,
                ShowDatabaseExtentTagsStatistics,
                ShowTableExtents,
                ShowTableExtentsMetadata,
                TableShardGroupsShow,
                TableShardGroupsStatisticsShow,
                DatabaseShardGroupsStatisticsShow,
                MergeExtentsDryrun,
                MergeExtents,
                MoveExtentsFrom,
                MoveExtentsQuery,
                ReplaceExtents,
                DropExtent,
                DropExtents,
                //DropExtentsQuery,
                //DropExtentsByProperties,
                DropPretendExtentsByProperties,
                ShowVersion,
                ClearTableData,
                ClearTableCacheStreamingIngestionSchema,
                #endregion

                // StoredQueryResults
                StoredQueryResultSet,
                StoredQueryResultsShow,
                StoredQueryResultShowSchema,
                StoredQueryResultDrop,
                StoredQueryResultsDrop,

                // extent containers
                AlterExtentContainersAdd,
                AlterExtentContainersDrop,
                AlterExtentContainersRecycle,
                AlterExtentContainersSet,
                ShowExtentContainers,
                DropEmptyExtentContainers,
                CleanDatabaseExtentContainers,
                ShowDatabaseExtentContainersCleanOperations,

                CreateRequestSupport,
                ShowRequestSupport,
                ShowClusterAdminState,
                ClearRemoteClusterDatabaseSchema,
                ShowClusterMonitoring,
                ShowClusterScaleIn,
                ShowClusterNetwork,
                AlterClusterStorageKeys,
                ShowClusterStorageKeysHash,
                ClearDatabaseCacheQueryResults,
                ShowDatabaseCacheQueryResults,
                ShowDatabasesManagementGroups,
                //ShowDatabasesPolicies,
                //ShowDatabasesPrincipals,
                AlterDatabaseStorageKeys,
                ClearDatabaseCacheStreamingIngestionSchema,
                ClearDatabaseCacheQueryWeakConsistency,
                ShowEntitySchema,
                ShowExtentDetails,
                ShowExtentColumnStorageStats,
                AttachExtentsIntoTableByContainer,
                AttachExtentsIntoTableByMetadata,
                AlterExtentTagsFromQuery,
                //DropExtentTagsFromQuery,
                //DropExtentTagsFromTable,
                DropExtentTagsRetention,
                SetExternalTableAdmins,
                AddExternalTableAdmins,
                DropExternalTableAdmins,
                AlterExternalTableDocString,
                AlterExternalTableFolder,
                ShowExternalTablePrincipals,
                ShowFabric,
                AlterFollowerClusterConfiguration,
                AddFollowerDatabaseAuthorizedPrincipals,
                DropFollowerDatabaseAuthorizedPrincipals,
                AlterFollowerDatabaseAuthorizedPrincipals,
                DropFollowerDatabasePolicyCaching,
                AlterFollowerDatabaseChildEntities,
                AlterFollowerDatabaseConfiguration,
                //AddFollowerDatabase,
                DropFollowerDatabases,
                ShowFollowerDatabase,
                AlterFollowerTablesPolicyCaching,
                DropFollowerTablesPolicyCaching,
                ShowFreshness,
                ShowFunctionSchemaAsJson,
                ShowIngestionMappings,

                SetMaterializedViewAdmins,
                AddMaterializedViewAdmins,
                DropMaterializedViewAdmins,
                SetMaterializedViewConcurrency,
                ClearMaterializedViewStatistics,
                ShowMaterializedViewStatistics,
                ShowMaterializedViewDiagnostics,
                ShowMaterializedViewFailures,
                ShowMemory,
                CancelOperation,
                DisablePlugin,
                EnablePlugin,
                ShowPlugins,
                ShowPrincipalAccess,
                ShowDatabasePurgeOperation,
                ShowQueryExecution,
                AlterPoliciesOfRetention,
                DeletePoliciesOfRetention,
                //AttachRowStore,
                CreateRowStore,
                DropRowStore,
                ShowRowStore,
                ShowRowStores,
                ShowRowStoreTransactions,
                ShowRowStoreSeals,
                ShowSchema,
                ShowCallStacks,
                ShowStreamingIngestionFailures,
                ShowStreamingIngestionStatistics,
                //AlterTableRowStoreReferences,
                AlterTableRowStoreReferencesDropKey,
                AlterTableRowStoreReferencesDropRowStore,
                AlterTableRowStoreReferencesDropBlockedKeys,
                AlterTableRowStoreReferencesDisableKey,
                AlterTableRowStoreReferencesDisableRowStore,
                AlterTableRowStoreReferencesDisableBlockedKeys,
                SetTableRowStoreReferences,
                ShowTableRowStoreReferences,
                AlterTableColumnStatistics,
                AlterTableColumnStatisticsMethod,
                ShowTableColumnStatitics,
                DropExtentsPartitionMetadata,
                //DropAsyncExtentsPartitionMetadata,  
                ShowTableDimensions,
                DeleteTableRecords,
                ShowTableColumnsClassification,
                ShowTableRowStores,
                ShowTableRowStoreSealInfo,
                //ShowDatabasesTablesPolicies,
                ShowTablesColumnStatistics,
                ShowTableUsageStatistics,
                ShowTableUsageStatisticsDetails,
                CreateTempStorage,
                DropTempStorage,
                DropStoredQueryResultContainers,
                DropUnusedStoredQueryResultContainers,
                EnableDatabaseMaintenanceMode,
                DisableDatabaseMaintenanceMode,
                ShowQueryCallTree,
            };
    }
}
