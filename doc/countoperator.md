---
title: count operator - Azure Data Explorer
description: Learn how to use the count operator to return the number of records in the input record set.
ms.reviewer: alexans
ms.topic: reference
ms.date: 11/23/2022
---
# count operator

Returns the number of records in the input record set.

## Syntax

`T | count`

## Arguments

*T*: The tabular data whose records are to be counted.

## Returns

This function returns a table with a single record and column of type
`long`. The value of the only cell is the number of records in *T*.

## Example

<!-- csl: https://help.kusto.windows.net/Samples -->
```kusto
StormEvents | count
```

## See also

For information about the count() aggregation function, see [count() (aggregation function)](count-aggfunction.md).
