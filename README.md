# Sloth Compiler in C&#35;

| | |
| --- | --- |
| **Build** | [![Build status](https://img.shields.io/appveyor/ci/drcjt/sloth.svg)](https://ci.appveyor.com/project/drcjt/sloth) |
[![Build History](https://buildstats.info/appveyor/chart/drcjt/sloth)](https://ci.appveyor.com/project/drcjt/sloth)

This is a reimagination of the "Good for Nothing" Compiler presented by Joel Pobar and Joe Duffy at PDC from 2005 using 
ideas from the Roslyn C#/VB compilers. For details on the original compiler see [here](https://blogs.msdn.microsoft.com/joelpob/2005/10/04/good-for-nothing-compiler-pdc-tln410-and-other-goodies/)

The key ideas here are to 

* explore the compiler as a service approach that the Roslyn C#/VB compilers use 
* explore using functional programming idoms in compiler design
* generate real executable output files

The code compiles a simple c-like language called "Sloth" and has support for variables, simple
inputs/outputs and a for-loop.

## Sloth Language Specification ##

We use Extended BNF with '[' and ']' for zero and once, and '{' and '}' for any number of repition including zero.

### Lexical Specifications ###
***Keywords***
	'var', 'for', 'to', 'do', 'end', 'read_int', 'print'
	
***Symbols***
	'=', '+', '-', '*', and '/'
	
***White characters***
	Space, tabulations, new line and line feed are the only white space characters supported.
	
***Strings***
```
	<string> := '"' { ? any character except double quote ? } '"'
```

***Identifiers***
	Identifiers start with a letter, followed by any number of alphanumeric characters plus the underscore. Identifiers are case sensitive.
```
	<ident> := ('a' .. 'z' | 'A' .. 'Z') {('a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '_')}
```

***Numbers***
	There are only numbers in Sloth.
```
	<int> := ('0' .. '9') { ('0' .. '9') }
```

***Invalid characters***
	Any other character is invalid.

### Syntactic Specifications ###
```
<prgm> := <stmt> { <stmt> }
<stmt> := 'var' <ident> '=' <expr> ;
	| <ident> '=' <expr> ;
	| 'for' <ident> '=' <expr> 'to' <expr> 'do' <stmt> 'end' ;
	| 'read_int' <ident> ;
	| 'print' <expr> ;

<expr> := <add_expr> ( ('+' | '-') <add_expr>)* 
<add_expr> := <primary_expr> ( ('*' | '/') <primary_expr)*
<primary_expr> := <string>
	| <int>
	| <ident>
```

#### Operator precedence ####

| Precedence | Operator | Description | Associativity |
| --- | --- | --- | --- |
| 1 | * / | Multiplication and division | Left-to-right |
| 2 | + - | Addition and subtraction | Left-to-right | 

### Semantics ###

TBD

### Sample Programs ###
A simple program could look like this:
```
var x = 2;
var y = 4;
var z = y / x;
print z;
print "that's it folks!";
```
A program with a loop could look like this:
```
var ntimes = 0;
print "How much do you love this company? (1-10) ";
read_int ntimes;
var x = 0;
for x = 0 to ntimes do
   print "Developers!";
end;
print "Who said sit down?!!!!!";
```
