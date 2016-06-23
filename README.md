# Sloth Compiler in C&#35;

This is a reimagination of the "Good for Nothing" Compiler presented by Joel Pobar and Joe Duffy at PDC from 2005 using 
ideas from the Roslyn C#/VB compilers. For details on the original compiler see [here](https://blogs.msdn.microsoft.com/joelpob/2005/10/04/good-for-nothing-compiler-pdc-tln410-and-other-goodies/)

The key ideas here are to 

* explore the compiler as a service approach that the Roslyn C#/VB compilers use 
* explore using functional programming idoms in compiler design
* generate real executable output files

The code compiles a simple c-like language called "Sloth" and has support for variables, simple
inputs/outputs and a for-loop.

##Language Specification##
This is the language specification defined in a simple EBNF style:

```
<stmt> := var <ident> = <expr>
	| <ident> = <expr>
	| for <ident> = <expr> to <expr> do <stmt> end
	| read_int <ident>
	| print <expr>
	| <stmt> ; <stmt>

<expr> := <string>
	| <int>
	| <arith_expr>
	| <ident>

<arith_expr> := <expr> <arith_op> <expr>
<arith_op> := + | - | * | /

<ident> := <char> <ident_rest>*
<ident_rest> := <char> | <digit>

<int> := <digit>+
<digit> := 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9

<string> := " <string_elem>* "
<string_elem> := <any char other than ">
```
##Sample Programs##
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
