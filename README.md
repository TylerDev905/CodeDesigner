# CodeDesigner

##Status
Small bugs but overall works pretty good. 

## Project Details
Code designer is a tool that can be used to assemble and disassemble Mips32 assembly code. Most if not all mips assembly instructions are supported as well as code designer syntax. Code designer syntax is a small set of commands that can make writting assembly much easier.

##Features
* Syntax highlighing of both the assembler and disassembler views
* Supports EE, COP0, and C0P1
* Code designer syntax
* The parser can detail errors down to the argument
* Single and multiline comments
* Multiple projects can be open at a time.

###Code designer syntax

#####Address 
will tell the assembler, the the address it should start counting from. Labels are not supported with the address command.
Address $000a0000

##Labels 
can be used to define an address with a human readable string. Labels can then be used with some operations and some Code desinger commands.
label: <--- a label can be used to set an address.
:targetlabel <---- target labels can be used to tell an instruction or command to use the labels address.

label:
addiu a0, a0, $0001
beq zero, zero, :label <--- will calculate the offet for the branch
nop
j :label <--will use the labels address for the jump operation

###Setreg 
will set a registers value to the word or label supplied.
setreg a0, $12345678
setreg a0, :label

###Call 
will pass the registers given and move them into argument registers then it will perform a jal instruction to the address or label specified.
call $12345678(s0, s1, s2, s3)
call :label(s0, s1, s2, s3)

###Hexcode 
will place a word at the current address of the assembler. The word can be defined in hex or it can come from a labels address.
hexcode $12345678
hecode :label

###Print will take a string and convert it into hexidecimal
print "hello world"

<img src="http://i.imgur.com/IS3dxgX.png"></img>
<img src="http://i.imgur.com/hYVFDx3.png"></img>
<img src="http://i.imgur.com/IHQw3It.png"></img>
<img src="http://i.imgur.com/LbUnAir.png"></img>
<img src="http://i.imgur.com/qTFXR1G.png"></img>
<img src="http://i.imgur.com/AYm9Nr6.png"></img>

