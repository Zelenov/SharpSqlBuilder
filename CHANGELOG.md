# Changelog

## 0.7.1
### Fixed
Select Refactoring

## 0.7
### Added
Aggregate Functions — Count, Every, Max, Min, Sum
Aggregate Functions and other operands in selects.

## 0.6
### Fixed
Auto brackets for all the operators (tested for postgres).
NotOperator
NotEqualsOperator
EqualsAllOperator

## 0.5.1
### Fixed
NotExistsOperator fix

## 0.5
### Added
EXISTS and NOT EXISTS Operators
Star (\*) Select

## 0.4.1
### Fixed
Operator Like returned

## 0.4
### Added
Operator ILike
Functions Abs, Acos, Asin, Atan, Atn2, Avg, Ceiling, Cos, Cot, Count, Degrees, Exp, Floor, Log, Log10, Lower, LTrim, Max, Min, Radians, Round, RTrim, Sign, Sin, Sqrt, Square, Sum, Tan, Trim, Upper

## 0.3.3
### Fixed
SqlInsertBuilder WHERE and RETURN operators order

## 0.3.2
### Added
LikeOperator

## 0.3.1
### Fixed
ParseOrderFilter accepts null arguments
AutoJoin index out of range fixed
IsFalseOperator, IsTrueOperator fixes
Insert WHERE and RETURNING blocks order fix

## 0.3.0
### Updated
RETURNING block now returns column as Property pairs, not just a list of columns

## 0.2.0
### Added
SqlTableTyped Class
SelectSqlBuilder.SplitOn Property