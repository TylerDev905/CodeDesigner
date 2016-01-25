using System;
using CodeDesigner;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeDesigner
{
    public class Mips32
    {
        public List<Register> Registers { get; set; }

        public List<Instruction> Instructions { get; set; }

        private _Assembler Assembler { get; set; }

        private _Disassembler Disassembler { get; set; }

        public Mips32()
        {
            Instructions = new List<Instruction>();

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111111111111111111111",
                ArgumentInfo = "32:NOP",
                Info = "No Operation",
                InstructionBin = "00000000000000000000000000000000",
                InstructionID = 1,
                MemoryID = MemoryMap.EE,
                Name = "NOP",
                Syntax = "NOP"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Add Word",
                InstructionBin = "00000000000000000000000000100000",
                InstructionID = 2,
                MemoryID = MemoryMap.EE,
                Name = "ADD",
                Syntax = "ADD rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt",
                Info = "Add Immediate Word",
                InstructionBin = "00000000000000000000000000000000",
                InstructionID = 3,
                MemoryID = MemoryMap.EE,
                Name = "ADDI",
                Syntax = "ADDI rt, rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:ADDIU;5:rs;5:rt",
                Info = "Add Immediate Unsigned Word",
                InstructionBin = "00100100000000000000000000000000",
                InstructionID = 4,
                MemoryID = MemoryMap.EE,
                Name = "ADDIU",
                Syntax = "ADDIU rt, rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Add Unsigned Word",
                InstructionBin = "00000000000000000000000000100001",
                InstructionID = 5,
                MemoryID = MemoryMap.EE,
                Name = "ADDU",
                Syntax = "ADDU rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "And",
                InstructionBin = "00000000000000000000000000100100",
                InstructionID = 6,
                MemoryID = MemoryMap.EE,
                Name = "AND",
                Syntax = "AND rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:ANDI;5:rs;5:rt",
                Info = "Add Immediate",
                InstructionBin = "00110000000000000000000000000000",
                InstructionID = 7,
                MemoryID = MemoryMap.EE,
                Name = "ANDI",
                Syntax = "ANDI rt, rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:BEQ;5:rs;5:rt",
                Info = "Branch on Equal",
                InstructionBin = "00010000000000000000000000000000",
                InstructionID = 8,
                MemoryID = MemoryMap.EE,
                Name = "BEQ",
                Syntax = "BEQ rs, rt, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:BEQL;5:rs;5:rt",
                Info = "Branch on Equal Likely",
                InstructionBin = "01010000000000000000000000000000",
                InstructionID = 9,
                MemoryID = MemoryMap.EE,
                Name = "BEQL",
                Syntax = "BEQL rs, rt, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:BGEZ",
                Info = "Branch on Greater Than or Equal to Zero",
                InstructionBin = "00000100000000010000000000000000",
                InstructionID = 10,
                MemoryID = MemoryMap.EE,
                Name = "BGEZ",
                Syntax = "BGEZ rs, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:BGEZAL",
                Info = "Branch on Greater Than or Equal to Zero and Link",
                InstructionBin = "00000100000100010000000000000000",
                InstructionID = 11,
                MemoryID = MemoryMap.EE,
                Name = "BGEZAL",
                Syntax = "BGEZAL rs, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:BGEZALL",
                Info = "Branch on Greater Than or Equal to Zero and Link Likely",
                InstructionBin = "00000100000100110000000000000000",
                InstructionID = 12,
                MemoryID = MemoryMap.EE,
                Name = "BGEZALL",
                Syntax = "BGEZALL rs, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:BGEZL",
                Info = "Branch on Greater Than or Equal to Zero Likely",
                InstructionBin = "00000100000000110000000000000000",
                InstructionID = 13,
                MemoryID = MemoryMap.EE,
                Name = "BGEZL",
                Syntax = "BGEZL rs, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:BGTZ;5:rs;5:0",
                Info = "Branch on Greater Than Zero",
                InstructionBin = "00011100000000000000000000000000",
                InstructionID = 14,
                MemoryID = MemoryMap.EE,
                Name = "BGTZ",
                Syntax = "BGTZ rs, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:BGTZL;5:rs;5:0",
                Info = "Branch on Greater Than Zero Likely",
                InstructionBin = "01011100000000000000000000000000",
                InstructionID = 15,
                MemoryID = MemoryMap.EE,
                Name = "BGTZL",
                Syntax = "BGTZL rs, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:BLEZ;5:rs;5:0",
                Info = "Branch on Less Than or Equal to Zero",
                InstructionBin = "00011000000000000000000000000000",
                InstructionID = 16,
                MemoryID = MemoryMap.EE,
                Name = "BLEZ",
                Syntax = "BLEZ rs, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:BLEZL;5:rs;5:0",
                Info = "Branch on Less Than or Equal to Zero Likely",
                InstructionBin = "01011000000000000000000000000000",
                InstructionID = 17,
                MemoryID = MemoryMap.EE,
                Name = "BLEZL",
                Syntax = "BLEZL rs, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:BLTZ",
                Info = "Branch on Less Than Zero",
                InstructionBin = "00000100000000000000000000000000",
                InstructionID = 18,
                MemoryID = MemoryMap.EE,
                Name = "BLTZ",
                Syntax = "BLTZ rs, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:BLTZAL",
                Info = "Branch on Less Than Zero and Link",
                InstructionBin = "00000100000100000000000000000000",
                InstructionID = 19,
                MemoryID = MemoryMap.EE,
                Name = "BLTZAL",
                Syntax = "BLTZAL rs, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:BLTZALL",
                Info = "Branch on Less Than Zero and Link Likely",
                InstructionBin = "00000100000100100000000000000000",
                InstructionID = 20,
                MemoryID = MemoryMap.EE,
                Name = "BLTZALL",
                Syntax = "BLTZALL rs, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:BLTZL",
                Info = "Branch on Less Than Zero Likely",
                InstructionBin = "00000100000000100000000000000000",
                InstructionID = 21,
                MemoryID = MemoryMap.EE,
                Name = "BLTZL",
                Syntax = "BLTZL rs, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:BNE;5:rs;5:rt",
                Info = "Branch on Not Equal",
                InstructionBin = "00010100000000000000000000000000",
                InstructionID = 22,
                MemoryID = MemoryMap.EE,
                Name = "BNE",
                Syntax = "BNE rs, rt, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:BNEL;5:rs;5:rt",
                Info = "Branch on Not Equal Likely",
                InstructionBin = "01010100000000000000000000000000",
                InstructionID = 23,
                MemoryID = MemoryMap.EE,
                Name = "BNEL",
                Syntax = "BNEL rs, rt, offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000111111",
                ArgumentInfo = "6:SPECIAL;20:code",
                Info = "Breakpoint",
                InstructionBin = "00000000000000000000000000001101",
                InstructionID = 24,
                MemoryID = MemoryMap.EE,
                Name = "BREAK",
                Syntax = "BREAK (code)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Doubleword Add",
                InstructionBin = "00000000000000000000000000101100",
                InstructionID = 25,
                MemoryID = MemoryMap.EE,
                Name = "DADD",
                Syntax = "DADD rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:DADDI;5:rs;5:rt",
                Info = "Doubleword Add Immediate",
                InstructionBin = "01100000000000000000000000000000",
                InstructionID = 26,
                MemoryID = MemoryMap.EE,
                Name = "DADDI",
                Syntax = "DADDI rt, rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:DADDIU;5:rs;5:rt",
                Info = "Doubleword Add Immediate Unsigned",
                InstructionBin = "01100100000000000000000000000000",
                InstructionID = 27,
                MemoryID = MemoryMap.EE,
                Name = "DADDIU",
                Syntax = "DADDIU rt, rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Doubleword Add Unsigned",
                InstructionBin = "00000000000000000000000000101101",
                InstructionID = 28,
                MemoryID = MemoryMap.EE,
                Name = "DADDU",
                Syntax = "DADDU rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000001111111111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;10:0",
                Info = "Divide Word",
                InstructionBin = "00000000000000000000000000011010",
                InstructionID = 29,
                MemoryID = MemoryMap.EE,
                Name = "DIV",
                Syntax = "DIV rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000001111111111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;10:0",
                Info = "Divide Unsigned Word",
                InstructionBin = "00000000000000000000000000011011",
                InstructionID = 30,
                MemoryID = MemoryMap.EE,
                Name = "DIVU",
                Syntax = "DIVU rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:0;5:rt;5:rd;5:sa",
                Info = "Doubleword Shift Left Logical",
                InstructionBin = "00000000000000000000000000111000",
                InstructionID = 31,
                MemoryID = MemoryMap.EE,
                Name = "DSLL",
                Syntax = "DSLL rd, rt, sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:0;5:rt;5:rd;5:sa",
                Info = "Doubleword Shift Left Logical Plus 32",
                InstructionBin = "00000000000000000000000000111100",
                InstructionID = 32,
                MemoryID = MemoryMap.EE,
                Name = "DSLL32",
                Syntax = "DSLL32 rd, rt, sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Doubleword Shift Left Logical Variable",
                InstructionBin = "00000000000000000000000000010100",
                InstructionID = 33,
                MemoryID = MemoryMap.EE,
                Name = "DSLLV",
                Syntax = "DSLLV rd, rt, rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:0;5:rt;5:rd;5:sa",
                Info = "Doubleword Shift Right Arithmetic",
                InstructionBin = "00000000000000000000000000111011",
                InstructionID = 34,
                MemoryID = MemoryMap.EE,
                Name = "DSRA",
                Syntax = "DSRA rd, rt, sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:0;5:rt;5:rd;5:sa",
                Info = "Doubleword Shift Right Arithmetic Plus 32",
                InstructionBin = "00000000000000000000000000111111",
                InstructionID = 35,
                MemoryID = MemoryMap.EE,
                Name = "DSRA32",
                Syntax = "DSRA32 rd, rt, sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Doubleword Shift Right Arithmetic Variable",
                InstructionBin = "00000000000000000000000000010111",
                InstructionID = 36,
                MemoryID = MemoryMap.EE,
                Name = "DSRAV",
                Syntax = "DSRAV rd, rt, rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:0;5:rt;5:rd;5:sa",
                Info = "Doubleword Shift Right Logical",
                InstructionBin = "00000000000000000000000000111010",
                InstructionID = 37,
                MemoryID = MemoryMap.EE,
                Name = "DSRL",
                Syntax = "DSRL rd, rt, sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:0;5:rt;5:rd;5:sa",
                Info = "Doubleword Shift Right Logical Plus 32",
                InstructionBin = "00000000000000000000000000111110",
                InstructionID = 38,
                MemoryID = MemoryMap.EE,
                Name = "DSRL32",
                Syntax = "DSRL32 rd, rt, sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Doubleword Shift Right Logical Variable",
                InstructionBin = "00000000000000000000000000010110",
                InstructionID = 39,
                MemoryID = MemoryMap.EE,
                Name = "DSRLV",
                Syntax = "DSRLV rd, rt, rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Doubleword Subtract",
                InstructionBin = "00000000000000000000000000101110",
                InstructionID = 40,
                MemoryID = MemoryMap.EE,
                Name = "DSUB",
                Syntax = "DSUB rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Doubleword Subtract Unsigned",
                InstructionBin = "00000000000000000000000000101111",
                InstructionID = 41,
                MemoryID = MemoryMap.EE,
                Name = "DSUBU",
                Syntax = "DSUBU rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:J",
                Info = "Jump",
                InstructionBin = "00001000000000000000000000000000",
                InstructionID = 42,
                MemoryID = MemoryMap.EE,
                Name = "J",
                Syntax = "J target"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:JAL",
                Info = "Jump and Link",
                InstructionBin = "00001100000000000000000000000000",
                InstructionID = 43,
                MemoryID = MemoryMap.EE,
                Name = "JAL",
                Syntax = "JAL target"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:0;5:rd;5:0;6:JALR",
                Info = "Jump and Link Register",
                InstructionBin = "00000000000000001111100000001001",
                InstructionID = 44,
                MemoryID = MemoryMap.EE,
                Name = "JALR",
                Syntax = "JALR rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:0;5:rd;5:0;6:JALR",
                Info = "Jump and Link Register",
                InstructionBin = "00000000000000001111100000001001",
                InstructionID = 45,
                MemoryID = MemoryMap.EE,
                Name = "JALR",
                Syntax = "JALR rd, rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111111111111111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;15:0",
                Info = "Jump Register",
                InstructionBin = "00000000000000000000000000001000",
                InstructionID = 46,
                MemoryID = MemoryMap.EE,
                Name = "JR",
                Syntax = "JR rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:LB;5:base;5:rt",
                Info = "Load Byte",
                InstructionBin = "10000000000000000000000000000000",
                InstructionID = 47,
                MemoryID = MemoryMap.EE,
                Name = "LB",
                Syntax = "LB rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:LBU;5:base;5:rt",
                Info = "Load Byte Unsigned",
                InstructionBin = "10010000000000000000000000000000",
                InstructionID = 48,
                MemoryID = MemoryMap.EE,
                Name = "LBU",
                Syntax = "LBU rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:LD;5:base;5:rt",
                Info = "Load Doubleword",
                InstructionBin = "11011100000000000000000000000000",
                InstructionID = 49,
                MemoryID = MemoryMap.EE,
                Name = "LD",
                Syntax = "LD rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:LDL;5:base;5:rt",
                Info = "Load Doubleword Left",
                InstructionBin = "01101000000000000000000000000000",
                InstructionID = 50,
                MemoryID = MemoryMap.EE,
                Name = "LDL",
                Syntax = "LDL rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:LDR;5:base;5:rt",
                Info = "Load Doubleword Right",
                InstructionBin = "01101100000000000000000000000000",
                InstructionID = 51,
                MemoryID = MemoryMap.EE,
                Name = "LDR",
                Syntax = "LDR rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:LH;5:base;5:rt",
                Info = "Load Halfword",
                InstructionBin = "10000100000000000000000000000000",
                InstructionID = 52,
                MemoryID = MemoryMap.EE,
                Name = "LH",
                Syntax = "LH rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:LHU;5:base;5:rt",
                Info = "Load Halfword Unsigned",
                InstructionBin = "10010100000000000000000000000000",
                InstructionID = 53,
                MemoryID = MemoryMap.EE,
                Name = "LHU",
                Syntax = "LHU rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000000000",
                ArgumentInfo = "6:LUI;5:0;5:rt",
                Info = "Load Upper Immediate",
                InstructionBin = "00111100000000000000000000000000",
                InstructionID = 54,
                MemoryID = MemoryMap.EE,
                Name = "LUI",
                Syntax = "LUI rt, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:LW;5:base;5:rt",
                Info = "Load Word",
                InstructionBin = "10001100000000000000000000000000",
                InstructionID = 55,
                MemoryID = MemoryMap.EE,
                Name = "LW",
                Syntax = "LW rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:LWL;5:base;5:rt",
                Info = "Load Word Left",
                InstructionBin = "10001000000000000000000000000000",
                InstructionID = 56,
                MemoryID = MemoryMap.EE,
                Name = "LWL",
                Syntax = "LWL rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:LWR;5:base;5:rt",
                Info = "Load Word Right",
                InstructionBin = "10011000000000000000000000000000",
                InstructionID = 57,
                MemoryID = MemoryMap.EE,
                Name = "LWR",
                Syntax = "LWR rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:LWU;5:base;5:rt",
                Info = "Load Word Unsigned",
                InstructionBin = "10011100000000000000000000000000",
                InstructionID = 58,
                MemoryID = MemoryMap.EE,
                Name = "LWU",
                Syntax = "LWU rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000011111111111",
                ArgumentInfo = "6:SPECIAL;10:0;5:rd;5:0",
                Info = "Move from HI Register",
                InstructionBin = "00000000000000000000000000010000",
                InstructionID = 59,
                MemoryID = MemoryMap.EE,
                Name = "MFHI",
                Syntax = "MFHI rd"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000011111111111",
                ArgumentInfo = "6:SPECIAL;10:0;5:rd;5:0",
                Info = "Move from LO Register",
                InstructionBin = "00000000000000000000000000010010",
                InstructionID = 60,
                MemoryID = MemoryMap.EE,
                Name = "MFLO",
                Syntax = "MFLO rd"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Move Conditional on Not Zero",
                InstructionBin = "00000000000000000000000000001011",
                InstructionID = 61,
                MemoryID = MemoryMap.EE,
                Name = "MOVN",
                Syntax = "MOVN rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Move Conditional on Zero",
                InstructionBin = "00000000000000000000000000001010",
                InstructionID = 62,
                MemoryID = MemoryMap.EE,
                Name = "MOVZ",
                Syntax = "MOVZ rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111111111111111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;15:0",
                Info = "Move to HI Register",
                InstructionBin = "00000000000000000000000000010001",
                InstructionID = 63,
                MemoryID = MemoryMap.EE,
                Name = "MTHI",
                Syntax = "MTHI rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111111111111111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;15:0",
                Info = "Move to LO Register",
                InstructionBin = "00000000000000000000000000010011",
                InstructionID = 64,
                MemoryID = MemoryMap.EE,
                Name = "MTLO",
                Syntax = "MTLO rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000001111111111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;10:0",
                Info = "Multiply Word",
                InstructionBin = "00000000000000000000000000011000",
                InstructionID = 65,
                MemoryID = MemoryMap.EE,
                Name = "MULT",
                Syntax = "MULT rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000001111111111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;10:0",
                Info = "Multiply Unsigned Word",
                InstructionBin = "00000000000000000000000000011001",
                InstructionID = 66,
                MemoryID = MemoryMap.EE,
                Name = "MULTU",
                Syntax = "MULTU rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Not Or",
                InstructionBin = "00000000000000000000000000100111",
                InstructionID = 67,
                MemoryID = MemoryMap.EE,
                Name = "NOR",
                Syntax = "NOR rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Or",
                InstructionBin = "00000000000000000000000000100101",
                InstructionID = 68,
                MemoryID = MemoryMap.EE,
                Name = "OR",
                Syntax = "OR rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:ORI;5:rs;5:rt",
                Info = "Or immediate",
                InstructionBin = "00110100000000000000000000000000",
                InstructionID = 69,
                MemoryID = MemoryMap.EE,
                Name = "ORI",
                Syntax = "ORI rt, rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:PREF;5:base;5:hint",
                Info = "Prefetch",
                InstructionBin = "11001100000000000000000000000000",
                InstructionID = 70,
                MemoryID = MemoryMap.EE,
                Name = "PREF",
                Syntax = "PREF hint, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:SB;5:base;5:rt",
                Info = "Store Byte",
                InstructionBin = "10100000000000000000000000000000",
                InstructionID = 71,
                MemoryID = MemoryMap.EE,
                Name = "SB",
                Syntax = "SB rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:SD;5:base;5:rt",
                Info = "Store Doubleword",
                InstructionBin = "11111100000000000000000000000000",
                InstructionID = 72,
                MemoryID = MemoryMap.EE,
                Name = "SD",
                Syntax = "SD rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:SDL;5:base;5:rt",
                Info = "Store Doubleword Left",
                InstructionBin = "10110000000000000000000000000000",
                InstructionID = 73,
                MemoryID = MemoryMap.EE,
                Name = "SDL",
                Syntax = "SDL rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:SDR;5:base;5:rt",
                Info = "Store Doubleword Right",
                InstructionBin = "10110100000000000000000000000000",
                InstructionID = 74,
                MemoryID = MemoryMap.EE,
                Name = "SDR",
                Syntax = "SDR rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:SH;5:base;5:rt",
                Info = "Store Halfword",
                InstructionBin = "10100100000000000000000000000000",
                InstructionID = 75,
                MemoryID = MemoryMap.EE,
                Name = "SH",
                Syntax = "SH rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:0;5:rt;5:rd;5:sa",
                Info = "Shift Word Left Logical",
                InstructionBin = "00000000000000000000000000000000",
                InstructionID = 76,
                MemoryID = MemoryMap.EE,
                Name = "SLL",
                Syntax = "SLL rd, rt, sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Shift Word Left Logical Variable",
                InstructionBin = "00000000000000000000000000000100",
                InstructionID = 77,
                MemoryID = MemoryMap.EE,
                Name = "SLLV",
                Syntax = "SLLV rd, rt, rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Set on Less Than",
                InstructionBin = "00000000000000000000000000101010",
                InstructionID = 78,
                MemoryID = MemoryMap.EE,
                Name = "SLT",
                Syntax = "SLT rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:SLTI;5:rs;5:rt",
                Info = "Set on Less Than Immediate",
                InstructionBin = "00101000000000000000000000000000",
                InstructionID = 79,
                MemoryID = MemoryMap.EE,
                Name = "SLTI",
                Syntax = "SLTI rt, rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:SLTIU;5:rs;5:rt",
                Info = "Set on Less Than Immediate Unsigned",
                InstructionBin = "00101100000000000000000000000000",
                InstructionID = 80,
                MemoryID = MemoryMap.EE,
                Name = "SLTIU",
                Syntax = "SLTIU rt, rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Set on Less Than Unsigned",
                InstructionBin = "00000000000000000000000000101011",
                InstructionID = 81,
                MemoryID = MemoryMap.EE,
                Name = "SLTU",
                Syntax = "SLTU rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:0;5:rt;5:rd;5:sa",
                Info = "Shift Word Right Arithmetic",
                InstructionBin = "00000000000000000000000000000011",
                InstructionID = 82,
                MemoryID = MemoryMap.EE,
                Name = "SRA",
                Syntax = "SRA rd, rt sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Shift Word Right Arithmetic Variable",
                InstructionBin = "00000000000000000000000000000111",
                InstructionID = 83,
                MemoryID = MemoryMap.EE,
                Name = "SRAV",
                Syntax = "SRAV rd, rt, rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:0;5:rt;5:rd;5:sa",
                Info = "Shift Word Right Logical",
                InstructionBin = "00000000000000000000000000000010",
                InstructionID = 84,
                MemoryID = MemoryMap.EE,
                Name = "SRL",
                Syntax = "SRL rd, rt, sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Shift Word Right Logical Variable",
                InstructionBin = "00000000000000000000000000000110",
                InstructionID = 85,
                MemoryID = MemoryMap.EE,
                Name = "SRLV",
                Syntax = "SRLV rd, rt, rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Subtract Word",
                InstructionBin = "00000000000000000000000000100010",
                InstructionID = 86,
                MemoryID = MemoryMap.EE,
                Name = "SUB",
                Syntax = "SUB rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Subtract Unsigned Word",
                InstructionBin = "00000000000000000000000000100011",
                InstructionID = 87,
                MemoryID = MemoryMap.EE,
                Name = "SUBU",
                Syntax = "SUBU rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:SW;5:base;5:rt",
                Info = "Store Word",
                InstructionBin = "10101100000000000000000000000000",
                InstructionID = 88,
                MemoryID = MemoryMap.EE,
                Name = "SW",
                Syntax = "SW rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:SWL;5:base;5:rt",
                Info = "Store Word Left",
                InstructionBin = "10101000000000000000000000000000",
                InstructionID = 89,
                MemoryID = MemoryMap.EE,
                Name = "SWL",
                Syntax = "SWL rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:SWR;5:base;5:rt",
                Info = "Store Word Right",
                InstructionBin = "10111000000000000000000000000000",
                InstructionID = 90,
                MemoryID = MemoryMap.EE,
                Name = "SWR",
                Syntax = "SWR rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111111111110000111111",
                ArgumentInfo = "6:SPECIAL;15:0;5:stype",
                Info = "Synchronize Shared Memory",
                InstructionBin = "00000000000000000000000000001111",
                InstructionID = 91,
                MemoryID = MemoryMap.EE,
                Name = "SYNC",
                Syntax = "SYNC"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111111111110000111111",
                ArgumentInfo = "6:SPECIAL;15:0;5:stype",
                Info = "Synchronize Shared Memory",
                InstructionBin = "00000000000000000000010000001111",
                InstructionID = 92,
                MemoryID = MemoryMap.EE,
                Name = "SYNC.P",
                Syntax = "SYNC.P"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000111111",
                ArgumentInfo = "6:SPECIAL;20:code",
                Info = "System Call",
                InstructionBin = "00000000000000000000000000001100",
                InstructionID = 93,
                MemoryID = MemoryMap.EE,
                Name = "SYSCALL",
                Syntax = "SYSCALL (code)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;10:code",
                Info = "Trap if Equal",
                InstructionBin = "00000000000000000000000000110100",

                InstructionID = 94,
                MemoryID = MemoryMap.EE,
                Name = "TEQ",
                Syntax = "TEQ rs, rt (code)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:TEQI",
                Info = "Trap if Equal Immediate",
                InstructionBin = "00000100000011000000000000000000",
                InstructionID = 95,
                MemoryID = MemoryMap.EE,
                Name = "TEQI",
                Syntax = "TEQI rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;10:code",
                Info = "Trap if Greater or Equal",
                InstructionBin = "00000000000000000000000000110000",
                InstructionID = 96,
                MemoryID = MemoryMap.EE,
                Name = "TGE",
                Syntax = "TGE rs, rt (code)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:TGEI",
                Info = "Trap if Greater or Equal Immediate",
                InstructionBin = "00000100000010000000000000000000",
                InstructionID = 97,
                MemoryID = MemoryMap.EE,
                Name = "TGEI",
                Syntax = "TGEI rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:TGEIU",
                Info = "Trap if Greater or Equal Immediate Unsigned",
                InstructionBin = "00000100000010010000000000000000",
                InstructionID = 98,
                MemoryID = MemoryMap.EE,
                Name = "TGEIU",
                Syntax = "TGEIU rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;10:code",
                Info = "Trap if Greater or Equal Unsigned",
                InstructionBin = "00000000000000000000000000110001",
                InstructionID = 99,
                MemoryID = MemoryMap.EE,
                Name = "TGEU",
                Syntax = "TGEU rs, rt (code)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;10:code",
                Info = "Trap if Less Than",
                InstructionBin = "00000000000000000000000000110010",
                InstructionID = 100,
                MemoryID = MemoryMap.EE,
                Name = "TLT",
                Syntax = "TLT rs, rt (code)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:TLTI",
                Info = "Trap if Less Than Immediate",
                InstructionBin = "00000100000010100000000000000000",
                InstructionID = 101,
                MemoryID = MemoryMap.EE,
                Name = "TLTI",
                Syntax = "TLTI rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:TLTIU",
                Info = "Trap if Less Than Immediate Unsigned",
                InstructionBin = "00000100000010110000000000000000",
                InstructionID = 102,
                MemoryID = MemoryMap.EE,
                Name = "TLTIU",
                Syntax = "TLTIU rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;10:code",
                Info = "Trap if Less Than Unsigned",
                InstructionBin = "00000000000000000000000000110011",
                InstructionID = 103,
                MemoryID = MemoryMap.EE,
                Name = "TLTU",
                Syntax = "TLTU rs, rt (code)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;10:code",
                Info = "Trap if Not Equal",
                InstructionBin = "00000000000000000000000000110110",
                InstructionID = 104,
                MemoryID = MemoryMap.EE,
                Name = "TNE",
                Syntax = "TNE rs, rt (code)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:TNEI",
                Info = "Trap if Not Equal Immediate",
                InstructionBin = "00000100000011100000000000000000",
                InstructionID = 105,
                MemoryID = MemoryMap.EE,
                Name = "TNEI",
                Syntax = "TNEI rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0",
                Info = "Exclusive OR",
                InstructionBin = "00000000000000000000000000100110",
                InstructionID = 106,
                MemoryID = MemoryMap.EE,
                Name = "XOR",
                Syntax = "XOR rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:XORI;5:rs;5:rt",
                Info = "Exclusive OR Immediate",
                InstructionBin = "00111000000000000000000000000000",
                InstructionID = 107,
                MemoryID = MemoryMap.EE,
                Name = "XORI",
                Syntax = "XORI rt, rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000001111111111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;10:0",
                Info = "Divide Word Pipeline 1",
                InstructionBin = "01110000000000000000000000011010",
                InstructionID = 108,
                MemoryID = MemoryMap.EE,
                Name = "DIV1",
                Syntax = "DIV1 rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000001111111111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;10:0",
                Info = "Divide Unsigned Word Pipeline 1",
                InstructionBin = "01110000000000000000000000011011",
                InstructionID = 109,
                MemoryID = MemoryMap.EE,
                Name = "DIVU1",
                Syntax = "DIVU1 rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:LQ;5:base;5:rt",
                Info = "Load Quadword",
                InstructionBin = "01111000000000000000000000000000",
                InstructionID = 110,
                MemoryID = MemoryMap.EE,
                Name = "LQ",
                Syntax = "LQ rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:0;6:MADD",
                Info = "Multiply-Add word",
                InstructionBin = "01110000000000000000000000000000",
                InstructionID = 111,
                MemoryID = MemoryMap.EE,
                Name = "MADD",
                Syntax = "MADD rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:0;6:MADD",
                Info = "Multiply-Add word",
                InstructionBin = "01110000000000000000000000000000",
                InstructionID = 112,
                MemoryID = MemoryMap.EE,
                Name = "MADD",
                Syntax = "MADD rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:0;6:MADD1",
                Info = "Multiply-Add word Pipeline 1",
                InstructionBin = "01110000000000000000000000100000",
                InstructionID = 113,
                MemoryID = MemoryMap.EE,
                Name = "MADD1",
                Syntax = "MADD1 rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:0;6:MADD1",
                Info = "Multiply-Add word Pipeline 1",
                InstructionBin = "01110000000000000000000000100000",
                InstructionID = 114,
                MemoryID = MemoryMap.EE,
                Name = "MADD1",
                Syntax = "MADD1 rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:0;6:MADDU",
                Info = "Multiply-Add Unsigned word",
                InstructionBin = "01110000000000000000000000000001",
                InstructionID = 115,
                MemoryID = MemoryMap.EE,
                Name = "MADDU",
                Syntax = "MADDU rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:0;6:MADDU",
                Info = "Multiply-Add Unsigned word",
                InstructionBin = "01110000000000000000000000000001",
                InstructionID = 116,
                MemoryID = MemoryMap.EE,
                Name = "MADDU",
                Syntax = "MADDU rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:0;6:MADDU1",
                Info = "Multiply-Add Unsigned word Pipeline 1",
                InstructionBin = "01110000000000000000000000100001",
                InstructionID = 117,
                MemoryID = MemoryMap.EE,
                Name = "MADDU1",
                Syntax = "MADDU1 rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:0;6:MADDU1",
                Info = "Multiply-Add Unsigned word Pipeline 1",
                InstructionBin = "01110000000000000000000000100001",
                InstructionID = 118,
                MemoryID = MemoryMap.EE,
                Name = "MADDU1",
                Syntax = "MADDU1 rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000011111111111",
                ArgumentInfo = "6:MMI;10:0;5:rd;5:0",
                Info = "Move From HI1 Register",
                InstructionBin = "01110000000000000000000000010000",
                InstructionID = 119,
                MemoryID = MemoryMap.EE,
                Name = "MFHI1",
                Syntax = "MFHI1 rd"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000011111111111",
                ArgumentInfo = "6:MMI;10:0;5:rd;5:0",
                Info = "Move From LO1 Register",
                InstructionBin = "01110000000000000000000000010010",
                InstructionID = 120,
                MemoryID = MemoryMap.EE,
                Name = "MFLO1",
                Syntax = "MFLO1 rd"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000011111111111",
                ArgumentInfo = "6:SPECIAL;10:0;5:rd;5:0",
                Info = "Move from Shift Amount Register",
                InstructionBin = "00000000000000000000000000101000",
                InstructionID = 121,
                MemoryID = MemoryMap.EE,
                Name = "MFSA",
                Syntax = "MFSA rd"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111111111111111111111",
                ArgumentInfo = "6:MMI;5:rs;15:0",
                Info = "Move To HI1 Register",
                InstructionBin = "01110000000000000000000000010001",
                InstructionID = 122,
                MemoryID = MemoryMap.EE,
                Name = "MTHI1",
                Syntax = "MTHI1 rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111111111111111111111",
                ArgumentInfo = "6:MMI;5:rs;15:0",
                Info = "Move To LO1 Register",
                InstructionBin = "01110000000000000000000000010001",
                InstructionID = 123,
                MemoryID = MemoryMap.EE,
                Name = "MTLO1",
                Syntax = "MTLO1 rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111111111111111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;15:0",
                Info = "Move to Shift Amount Register",
                InstructionBin = "00000000000000000000000000101001",
                InstructionID = 124,
                MemoryID = MemoryMap.EE,
                Name = "MTSA",
                Syntax = "MTSA rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:11000",
                Info = "Move Byte Count to Shift Amount Register",
                InstructionBin = "00000100000110000000000000000000",
                InstructionID = 125,
                MemoryID = MemoryMap.EE,
                Name = "MTSAB",
                Syntax = "MTSAB rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000000000000000",
                ArgumentInfo = "6:REGIMM;5:rs;5:11001",
                Info = "Move Halfword Count to Shift Amount Register",
                InstructionBin = "00000100000110010000000000000000",
                InstructionID = 126,
                MemoryID = MemoryMap.EE,
                Name = "MTSAH",
                Syntax = "MTSAH rs, immediate"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0;6:MULT",
                Info = "Multiply Word",
                InstructionBin = "00000000000000000000000000011000",
                InstructionID = 127,
                MemoryID = MemoryMap.EE,
                Name = "MULT",
                Syntax = "MULT rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0;6:MULT",
                Info = "Multiply Word",
                InstructionBin = "00000000000000000000000000011000",
                InstructionID = 128,
                MemoryID = MemoryMap.EE,
                Name = "MULT",
                Syntax = "MULT rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:0;6:MULT1",
                Info = "Multiply Word Pipeline 1",
                InstructionBin = "01110000000000000000000000011000",
                InstructionID = 129,
                MemoryID = MemoryMap.EE,
                Name = "MULT1",
                Syntax = "MULT1 rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:0;6:MULT1",
                Info = "Multiply Word Pipeline 1",
                InstructionBin = "01110000000000000000000000011000",
                InstructionID = 130,
                MemoryID = MemoryMap.EE,
                Name = "MULT1",
                Syntax = "MULT1 rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0;6:MULTU",
                Info = "Multiply Unsigned Word",
                InstructionBin = "00000000000000000000000000011001",
                InstructionID = 131,
                MemoryID = MemoryMap.EE,
                Name = "MULTU",
                Syntax = "MULTU rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:SPECIAL;5:rs;5:rt;5:rd;5:0;6:MULTU",
                Info = "Multiply Unsigned Word",
                InstructionBin = "00000000000000000000000000011001",
                InstructionID = 132,
                MemoryID = MemoryMap.EE,
                Name = "MULTU",
                Syntax = "MULTU rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:0;6:MULTU1",
                Info = "Multiply Unsigned Word Pipeline 1",
                InstructionBin = "01110000000000000000000000011001",
                InstructionID = 133,
                MemoryID = MemoryMap.EE,
                Name = "MULTU1",
                Syntax = "MULTU1 rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:0;6:MULTU1",
                Info = "Multiply Unsigned Word Pipeline 1",
                InstructionBin = "01110000000000000000000000011001",
                InstructionID = 134,
                MemoryID = MemoryMap.EE,
                Name = "MULTU1",
                Syntax = "MULTU1 rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:PABSH",
                Info = "Parallel Absolute Halfword",
                InstructionBin = "01110000000000000000000101101000",
                InstructionID = 135,
                MemoryID = MemoryMap.EE,
                Name = "PABSH",
                Syntax = "PABSH rd, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:PABSW",
                Info = "Parallel Absolute Word",
                InstructionBin = "01110000000000000000000001101000",
                InstructionID = 136,
                MemoryID = MemoryMap.EE,
                Name = "PABSW",
                Syntax = "PABSW rd, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PADDB",
                Info = "Parallel Add Byte",
                InstructionBin = "01110000000000000000001000001000",
                InstructionID = 137,
                MemoryID = MemoryMap.EE,
                Name = "PADDB",
                Syntax = "PADDB rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PADDH",
                Info = "Parallel Add Halfword",
                InstructionBin = "01110000000000000000000100001000",
                InstructionID = 138,
                MemoryID = MemoryMap.EE,
                Name = "PADDH",
                Syntax = "PADDH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PADDSB",
                Info = "Parallel Add with Signed saturation Byte",
                InstructionBin = "01110000000000000000011000001000",
                InstructionID = 139,
                MemoryID = MemoryMap.EE,
                Name = "PADDSB",
                Syntax = "PADDSB rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PADDSH",
                Info = "Parallel Add with Signed saturation Halfword",
                InstructionBin = "01110000000000000000010100001000",
                InstructionID = 140,
                MemoryID = MemoryMap.EE,
                Name = "PADDSH",
                Syntax = "PADDSH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PADDSW",
                Info = "Parallel Add with Signed saturation Word",
                InstructionBin = "01110000000000000000010000001000",
                InstructionID = 141,
                MemoryID = MemoryMap.EE,
                Name = "PADDSW",
                Syntax = "PADDSW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PADDUB",
                Info = "Parallel Add with Unsigned saturation Byte",
                InstructionBin = "01110000000000000000011000101000",
                InstructionID = 142,
                MemoryID = MemoryMap.EE,
                Name = "PADDUB",
                Syntax = "PADDUB rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PADDUH",
                Info = "Parallel Add with Unsigned saturation Halfword",
                InstructionBin = "01110000000000000000010100101000",
                InstructionID = 143,
                MemoryID = MemoryMap.EE,
                Name = "PADDUH",
                Syntax = "PADDUH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PADDUW",
                Info = "Parallel Add with Unsigned saturation Word",
                InstructionBin = "01110000000000000000010000101000",
                InstructionID = 144,
                MemoryID = MemoryMap.EE,
                Name = "PADDUW",
                Syntax = "PADDUW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PADDW",
                Info = "Parallel Add Word",
                InstructionBin = "01110000000000000000000000001000",
                InstructionID = 145,
                MemoryID = MemoryMap.EE,
                Name = "PADDW",
                Syntax = "PADDW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PADSBH",
                Info = "Parallel Add/Subtract Halfword",
                InstructionBin = "01110000000000000000000100101000",
                InstructionID = 146,
                MemoryID = MemoryMap.EE,
                Name = "PADSBH",
                Syntax = "PADSBH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PAND",
                Info = "Parallel And",
                InstructionBin = "01110000000000000000010010001001",
                InstructionID = 147,
                MemoryID = MemoryMap.EE,
                Name = "PAND",
                Syntax = "PAND rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PCEQB",
                Info = "Parallel Compare for Equal Byte",
                InstructionBin = "01110000000000000000001010101000",
                InstructionID = 148,
                MemoryID = MemoryMap.EE,
                Name = "PCEQB",
                Syntax = "PCEQB rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PCEQH",
                Info = "Parallel Compare for Equal Halfword",
                InstructionBin = "01110000000000000000000110101000",
                InstructionID = 149,
                MemoryID = MemoryMap.EE,
                Name = "PCEQH",
                Syntax = "PCEQH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PCEQW",
                Info = "Parallel Compare for Equal Word",
                InstructionBin = "01110000000000000000000010101000",
                InstructionID = 150,
                MemoryID = MemoryMap.EE,
                Name = "PCEQW",
                Syntax = "PCEQW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PCGTB",
                Info = "Parallel Compare for Greater Than Byte",
                InstructionBin = "01110000000000000000001010001000",
                InstructionID = 151,
                MemoryID = MemoryMap.EE,
                Name = "PCGTB",
                Syntax = "PCGTB rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PCGTH",
                Info = "Parallel Compare for Greater Than Halfword",
                InstructionBin = "01110000000000000000000110001000",
                InstructionID = 152,
                MemoryID = MemoryMap.EE,
                Name = "PCGTH",
                Syntax = "PCGTH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PCGTW",
                Info = "Parallel Compare for Greater Than Word",
                InstructionBin = "01110000000000000000000010001000",
                InstructionID = 153,
                MemoryID = MemoryMap.EE,
                Name = "PCGTW",
                Syntax = "PCGTW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:PCPYH",
                Info = "Parallel Copy Halfword",
                InstructionBin = "01110000000000000000011011101001",
                InstructionID = 154,
                MemoryID = MemoryMap.EE,
                Name = "PCPYH",
                Syntax = "PCPYH rd, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PCPYLD",
                Info = "Parallel Copy Lower Doubleword",
                InstructionBin = "01110000000000000000001110001001",
                InstructionID = 155,
                MemoryID = MemoryMap.EE,
                Name = "PCPYLD",
                Syntax = "PCPYLD rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PCPYUD",
                Info = "Parallel Copy Upper Doubleword",
                InstructionBin = "01110000000000000000001110101001",
                InstructionID = 156,
                MemoryID = MemoryMap.EE,
                Name = "PCPYUD",
                Syntax = "PCPYUD rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000001111111111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:0;5:PDIVBW",
                Info = "Parallel Divide Broadcast Word",
                InstructionBin = "01110000000000000000011101001001",
                InstructionID = 157,
                MemoryID = MemoryMap.EE,
                Name = "PDIVBW",
                Syntax = "PDIVBW rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000001111111111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:0;5:PDIVUW",
                Info = "Parallel Divide Unsigned Word",
                InstructionBin = "01110000000000000000001101101001",
                InstructionID = 158,
                MemoryID = MemoryMap.EE,
                Name = "PDIVUW",
                Syntax = "PDIVUW rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000001111111111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:0;5:PDIVW",
                Info = "Parallel Divide Word",
                InstructionBin = "01110000000000000000001101001001",
                InstructionID = 159,
                MemoryID = MemoryMap.EE,
                Name = "PDIVW",
                Syntax = "PDIVW rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:PEXCH",
                Info = "Parallel Exchange Center Halfword",
                InstructionBin = "01110000000000000000011010101001",
                InstructionID = 160,
                MemoryID = MemoryMap.EE,
                Name = "PEXCH",
                Syntax = "PEXCH rd, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:PEXCW",
                Info = "Parallel Exchange Center Word",
                InstructionBin = "01110000000000000000011110101001",
                InstructionID = 161,
                MemoryID = MemoryMap.EE,
                Name = "PEXCW",
                Syntax = "PEXCW rd, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:PEXEH",
                Info = "Parallel Exchange Even Halfword",
                InstructionBin = "01110000000000000000011010001001",
                InstructionID = 162,
                MemoryID = MemoryMap.EE,
                Name = "PEXEH",
                Syntax = "PEXEH rd, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:PEXEW",
                Info = "Parallel Exchange Even Word",
                InstructionBin = "01110000000000000000011110001001",
                InstructionID = 163,
                MemoryID = MemoryMap.EE,
                Name = "PEXEW",
                Syntax = "PEXEW rd, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:PEXT5",
                Info = "Parallel Extend from 5 bits",
                InstructionBin = "01110000000000000000011110001000",
                InstructionID = 164,
                MemoryID = MemoryMap.EE,
                Name = "PEXT5",
                Syntax = "PEXT5 rd, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PEXTLB",
                Info = "Parallel Extend Lower from Byte",
                InstructionBin = "01110000000000000000011010001000",
                InstructionID = 165,
                MemoryID = MemoryMap.EE,
                Name = "PEXTLB",
                Syntax = "PEXTLB rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PEXTLH",
                Info = "Parallel Extend Lower from Halfword",
                InstructionBin = "01110000000000000000010110001000",
                InstructionID = 166,
                MemoryID = MemoryMap.EE,
                Name = "PEXTLH",
                Syntax = "PEXTLH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PEXTLW",
                Info = "Parallel Extend Lower from Word",
                InstructionBin = "01110000000000000000010010001000",
                InstructionID = 167,
                MemoryID = MemoryMap.EE,
                Name = "PEXTLW",
                Syntax = "PEXTLW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PEXTUB",
                Info = "Parallel Extend Upper from Byte",
                InstructionBin = "01110000000000000000011010101000",
                InstructionID = 168,
                MemoryID = MemoryMap.EE,
                Name = "PEXTUB",
                Syntax = "PEXTUB rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PEXTUH",
                Info = "Parallel Extend Upper from Halfword",
                InstructionBin = "01110000000000000000010110101000",
                InstructionID = 169,
                MemoryID = MemoryMap.EE,
                Name = "PEXTUH",
                Syntax = "PEXTUH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PEXTUW",
                Info = "Parallel Extend Upper from Word",
                InstructionBin = "01110000000000000000010010101000",
                InstructionID = 170,
                MemoryID = MemoryMap.EE,
                Name = "PEXTUW",
                Syntax = "PEXTUW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PHMADH",
                Info = "Parallel Horizontal Multiply-Add Halfword",
                InstructionBin = "01110000000000000000010001001001",
                InstructionID = 171,
                MemoryID = MemoryMap.EE,
                Name = "PHMADH",
                Syntax = "PHMADH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PHMSBH",
                Info = "Parallel Horizontal Multiply-Subtract Halfword",
                InstructionBin = "01110000000000000000010101001001",
                InstructionID = 172,
                MemoryID = MemoryMap.EE,
                Name = "PHMSBH",
                Syntax = "PHMSBH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PINTEH",
                Info = "Parallel Interleave Even Halfword",
                InstructionBin = "01110000000000000000001010101001",
                InstructionID = 173,
                MemoryID = MemoryMap.EE,
                Name = "PINTEH",
                Syntax = "PINTEH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PINTH",
                Info = "Parallel Interleave Halfword",
                InstructionBin = "01110000000000000000001010001001",
                InstructionID = 174,
                MemoryID = MemoryMap.EE,
                Name = "PINTH",
                Syntax = "PINTH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111110000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:0;5:rd;5:0",
                Info = "Parallel Leading Zero or one Count Word",
                InstructionBin = "01110000000000000000000000000100",
                InstructionID = 175,
                MemoryID = MemoryMap.EE,
                Name = "PLZCW",
                Syntax = "PLZCW rd, rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PMADDH",
                Info = "Parallel Multiply-Add Halfword",
                InstructionBin = "01110000000000000000010000001001",
                InstructionID = 176,
                MemoryID = MemoryMap.EE,
                Name = "PMADDH",
                Syntax = "PMADDH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PMADDUW",
                Info = "Parallel Multiply-Add Unsigned Word",
                InstructionBin = "01110000000000000000000000101001",
                InstructionID = 177,
                MemoryID = MemoryMap.EE,
                Name = "PMADDUW",
                Syntax = "PMADDUW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PMADDW",
                Info = "Parallel Multiply-Add Word",
                InstructionBin = "01110000000000000000000000001001",
                InstructionID = 178,
                MemoryID = MemoryMap.EE,
                Name = "PMADDW",
                Syntax = "PMADDW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PMAXH",
                Info = "Parallel Maximize Halfword",
                InstructionBin = "01110000000000000000000111001000",
                InstructionID = 179,
                MemoryID = MemoryMap.EE,
                Name = "PMAXH",
                Syntax = "PMAXH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PMAXW",
                Info = "Parallel Maximize Word",
                InstructionBin = "01110000000000000000000011001000",
                InstructionID = 180,
                MemoryID = MemoryMap.EE,
                Name = "PMAXW",
                Syntax = "PMAXW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000011111111111",
                ArgumentInfo = "6:MMI;10:0;5:rd;5:PMFHI",
                Info = "Parallel Move From HI Register",
                InstructionBin = "01110000000000000000001000001001",
                InstructionID = 181,
                MemoryID = MemoryMap.EE,
                Name = "PMFHI",
                Syntax = "PMFHI rd"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000011111111111",
                ArgumentInfo = "6:MMI;10:0;5:rd;5:fmt",
                Info = "Parallel Move From HI/LO Register",
                InstructionBin = "01110000000000000000000011110000",
                InstructionID = 182,
                MemoryID = MemoryMap.EE,
                Name = "PMFHL.LH",
                Syntax = "PMFHL.LH rd"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000011111111111",
                ArgumentInfo = "6:MMI;10:0;5:rd;5:fmt",
                Info = "Parallel Move From HI/LO Register",
                InstructionBin = "01110000000000000000000000110000",
                InstructionID = 183,
                MemoryID = MemoryMap.EE,
                Name = "PMFHL.LW",
                Syntax = "PMFHL.LW rd"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000011111111111",
                ArgumentInfo = "6:MMI;10:0;5:rd;5:fmt",
                Info = "Parallel Move From HI/LO Register",
                InstructionBin = "01110000000000000000000100110000",
                InstructionID = 184,
                MemoryID = MemoryMap.EE,
                Name = "PMFHL.SH",
                Syntax = "PMFHL.SH rd"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000011111111111",
                ArgumentInfo = "6:MMI;10:0;5:rd;5:fmt",
                Info = "Parallel Move From HI/LO Register",
                InstructionBin = "01110000000000000000000010110000",
                InstructionID = 185,
                MemoryID = MemoryMap.EE,
                Name = "PMFHL.SLW",
                Syntax = "PMFHL.SLW rd"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000011111111111",
                ArgumentInfo = "6:MMI;10:0;5:rd;5:fmt",
                Info = "Parallel Move From HI/LO Register",
                InstructionBin = "01110000000000000000000001110000",
                InstructionID = 186,
                MemoryID = MemoryMap.EE,
                Name = "PMFHL.UW",
                Syntax = "PMFHL.UW rd"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000011111111111",
                ArgumentInfo = "6:MMI;10:0;5:rd;5:PMFLO",
                Info = "Parallel Move From LO Register",
                InstructionBin = "01110000000000000000001001001001",
                InstructionID = 187,
                MemoryID = MemoryMap.EE,
                Name = "PMFLO",
                Syntax = "PMFLO rd"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PMINH",
                Info = "Parallel Minimize Halfword",
                InstructionBin = "01110000000000000000000111101000",
                InstructionID = 188,
                MemoryID = MemoryMap.EE,
                Name = "PMINH",
                Syntax = "PMINH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PMINW",
                Info = "Parallel Minimize Word",
                InstructionBin = "01110000000000000000000011101000",
                InstructionID = 189,
                MemoryID = MemoryMap.EE,
                Name = "PMINW",
                Syntax = "PMINW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PMSUBH",
                Info = "Parallel Multiply-Subtract Halfword",
                InstructionBin = "01110000000000000000010100001001",
                InstructionID = 190,
                MemoryID = MemoryMap.EE,
                Name = "PMSUBH",
                Syntax = "PMSUBH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PMSUBW",
                Info = "Parallel Multiply-Subtract Word",
                InstructionBin = "01110000000000000000000100001001",
                InstructionID = 191,
                MemoryID = MemoryMap.EE,
                Name = "PMSUBW",
                Syntax = "PMSUBW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111111111111111111111",
                ArgumentInfo = "6:MMI;5:rs;10:0;5:PMTHI",
                Info = "Parallel Move To HI Register",
                InstructionBin = "01110000000000000000001000101001",
                InstructionID = 192,
                MemoryID = MemoryMap.EE,
                Name = "PMTHI",
                Syntax = "PMTHI rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111111111111111111111",
                ArgumentInfo = "6:MMI;5:rs;10:0;5:fmt",
                Info = "Parallel Move To HI/LO Register",
                InstructionBin = "01110000000000000000000000110001",
                InstructionID = 193,
                MemoryID = MemoryMap.EE,
                Name = "PMTHL.LW",
                Syntax = "PMTHL.LW rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000111111111111111111111",
                ArgumentInfo = "6:MMI;5:rs;10:0;5:PMTLO",
                Info = "Parallel Move To LO Register",
                InstructionBin = "01110000000000000000001001101001",
                InstructionID = 194,
                MemoryID = MemoryMap.EE,
                Name = "PMTLO",
                Syntax = "PMTLO rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PMULTH",
                Info = "Parallel Multiply Halfword",
                InstructionBin = "01110000000000000000011100001001",
                InstructionID = 195,
                MemoryID = MemoryMap.EE,
                Name = "PMULTH",
                Syntax = "PMULTH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PMULTUW",
                Info = "Parallel Multiply Unsigned Word",
                InstructionBin = "01110000000000000000001100101001",
                InstructionID = 196,
                MemoryID = MemoryMap.EE,
                Name = "PMULTUW",
                Syntax = "PMULTUW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PMULTW",
                Info = "Parallel Multiply Word",
                InstructionBin = "01110000000000000000001100001001",
                InstructionID = 197,
                MemoryID = MemoryMap.EE,
                Name = "PMULTW",
                Syntax = "PMULTW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PNOR",
                Info = "Parallel Not Or",
                InstructionBin = "01110000000000000000010011101001",
                InstructionID = 198,
                MemoryID = MemoryMap.EE,
                Name = "PNOR",
                Syntax = "PNOR rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:POR",
                Info = "Parallel Or",
                InstructionBin = "01110000000000000000010010101001",
                InstructionID = 199,
                MemoryID = MemoryMap.EE,
                Name = "POR",
                Syntax = "POR rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:PPAC5",
                Info = "Parallel Pack to 5 bits",
                InstructionBin = "01110000000000000000011111001000",
                InstructionID = 200,
                MemoryID = MemoryMap.EE,
                Name = "PPAC5",
                Syntax = "PPAC5 rd, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PPACB",
                Info = "Parallel Pack to Byte",
                InstructionBin = "01110000000000000000011011001000",
                InstructionID = 201,
                MemoryID = MemoryMap.EE,
                Name = "PPACB",
                Syntax = "PPACB rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PPACH",
                Info = "Parallel Pack to Halfword",
                InstructionBin = "01110000000000000000010111001000",
                InstructionID = 202,
                MemoryID = MemoryMap.EE,
                Name = "PPACH",
                Syntax = "PPACH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PPACW",
                Info = "Parallel Pack to Word",
                InstructionBin = "01110000000000000000010011001000",
                InstructionID = 203,
                MemoryID = MemoryMap.EE,
                Name = "PPACW",
                Syntax = "PPACW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:PREVH",
                Info = "Parallel Reverse Halfword",
                InstructionBin = "01110000000000000000011011001001",
                InstructionID = 204,
                MemoryID = MemoryMap.EE,
                Name = "PREVH",
                Syntax = "PREVH rd, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:PROT3W",
                Info = "Parallel Rotate 3 Words Left",
                InstructionBin = "01110000000000000000011111001001",
                InstructionID = 205,
                MemoryID = MemoryMap.EE,
                Name = "PROT3W",
                Syntax = "PROT3W rd, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:sa",
                Info = "Parallel Shift Left Logical Halfword",
                InstructionBin = "01110000000000000000000000110100",
                InstructionID = 206,
                MemoryID = MemoryMap.EE,
                Name = "PSLLH",
                Syntax = "PSLLH rd, rt, sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PSLLVW",
                Info = "Parallel Shift Left Logical Variable Word",
                InstructionBin = "01110000000000000000000010001001",
                InstructionID = 207,
                MemoryID = MemoryMap.EE,
                Name = "PSLLVW",
                Syntax = "PSLLVW rd, rt, rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:sa",
                Info = "Parallel Shift Left Logical Word",
                InstructionBin = "01110000000000000000000000111100",
                InstructionID = 208,
                MemoryID = MemoryMap.EE,
                Name = "PSLLW",
                Syntax = "PSLLW rd, rt, sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:sa",
                Info = "Parallel Shift Right Arithmetic Halfword",
                InstructionBin = "01110000000000000000000000110111",
                InstructionID = 209,
                MemoryID = MemoryMap.EE,
                Name = "PSRAH",
                Syntax = "PSRAH rd, rt, sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PSRAVW",
                Info = "Parallel Shift Right Arithmetic Variable Word",
                InstructionBin = "01110000000000000000000011101001",
                InstructionID = 210,
                MemoryID = MemoryMap.EE,
                Name = "PSRAVW",
                Syntax = "PSRAVW rd, rt, rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:sa",
                Info = "Parallel Shift Right Arithmetic Word",
                InstructionBin = "01110000000000000000000000111111",
                InstructionID = 211,
                MemoryID = MemoryMap.EE,
                Name = "PSRAW",
                Syntax = "PSRAW rd, rt, sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:sa",
                Info = "Parallel Shift Right Logical Halfword",
                InstructionBin = "01110000000000000000000000110110",
                InstructionID = 212,
                MemoryID = MemoryMap.EE,
                Name = "PSRLH",
                Syntax = "PSRLH rd, rt, sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PSRLVW",
                Info = "Parallel Shift Right Logical Variable Word",
                InstructionBin = "01110000000000000000000011001001",
                InstructionID = 213,
                MemoryID = MemoryMap.EE,
                Name = "PSRLVW",
                Syntax = "PSRLVW rd, rt, rs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:MMI;5:0;5:rt;5:rd;5:sa",
                Info = "Parallel Shift Right Logical Word",
                InstructionBin = "01110000000000000000000000111110",
                InstructionID = 214,
                MemoryID = MemoryMap.EE,
                Name = "PSRLW",
                Syntax = "PSRLW rd, rt, sa"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PSUBB",
                Info = "Parallel Subtract Byte",
                InstructionBin = "01110000000000000000001001001000",
                InstructionID = 215,
                MemoryID = MemoryMap.EE,
                Name = "PSUBB",
                Syntax = "PSUBB rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PSUBH",
                Info = "Parallel Subtract Halfword",
                InstructionBin = "01110000000000000000000101001000",
                InstructionID = 216,
                MemoryID = MemoryMap.EE,
                Name = "PSUBH",
                Syntax = "PSUBH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PSUBSB",
                Info = "Parallel Subtract with Signed saturation Byte",
                InstructionBin = "01110000000000000000011001001000",
                InstructionID = 217,
                MemoryID = MemoryMap.EE,
                Name = "PSUBSB",
                Syntax = "PSUBSB rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PSUBSH",
                Info = "Parallel Subtract with Signed Saturation Halfword",
                InstructionBin = "01110000000000000000010101001000",
                InstructionID = 218,
                MemoryID = MemoryMap.EE,
                Name = "PSUBSH",
                Syntax = "PSUBSH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PSUBSW",
                Info = "Parallel Subtract with Signed Saturation Word",
                InstructionBin = "01110000000000000000010001001000",
                InstructionID = 219,
                MemoryID = MemoryMap.EE,
                Name = "PSUBSW",
                Syntax = "PSUBSW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PSUBUB",
                Info = "Parallel Subtract with Unsigned Saturation Byte",
                InstructionBin = "01110000000000000000011001101000",
                InstructionID = 220,
                MemoryID = MemoryMap.EE,
                Name = "PSUBUB",
                Syntax = "PSUBUB rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PSUBUH",
                Info = "Parallel Subtract with Unsigned Saturation Halfword",
                InstructionBin = "01110000000000000000010101101000",
                InstructionID = 221,
                MemoryID = MemoryMap.EE,
                Name = "PSUBUH",
                Syntax = "PSUBUH rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PSUBUW",
                Info = "Parallel Subtract with Unsigned Saturation Word",
                InstructionBin = "01110000000000000000010001101000",
                InstructionID = 222,
                MemoryID = MemoryMap.EE,
                Name = "PSUBUW",
                Syntax = "PSUBUW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PSUBW",
                Info = "Parallel Subtract Word",
                InstructionBin = "01110000000000000000000001001000",
                InstructionID = 223,
                MemoryID = MemoryMap.EE,
                Name = "PSUBW",
                Syntax = "PSUBW rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:PXOR",
                Info = "Parallel Exclusive OR",
                InstructionBin = "01110000000000000000010011001001",
                InstructionID = 224,
                MemoryID = MemoryMap.EE,
                Name = "PXOR",
                Syntax = "PXOR rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000011111111111",
                ArgumentInfo = "6:MMI;5:rs;5:rt;5:rd;5:QFSRV",
                Info = "Quadword Funnel Shift Right Variable",
                InstructionBin = "01110000000000000000011011101000",
                InstructionID = 225,
                MemoryID = MemoryMap.EE,
                Name = "QFSRV",
                Syntax = "QFSRV rd, rs, rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:SQ;5:base;5:rt",
                Info = "Store Quadword",
                InstructionBin = "01111100000000000000000000000000",
                InstructionID = 226,
                MemoryID = MemoryMap.EE,
                Name = "SQ",
                Syntax = "SQ rt, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000000000000000",
                ArgumentInfo = "6:COP0;5:BC0;5:BC0F",
                Info = "Branch on Coprocessor 0 False",
                InstructionBin = "01000001000000000000000000000000",
                InstructionID = 227,
                MemoryID = MemoryMap.COP0,
                Name = "BC0F",
                Syntax = "BC0F offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000000000000000",
                ArgumentInfo = "6:COP0;5:BC0;5:BC0FL",
                Info = "Branch on Coprocessor 0 False Likely",
                InstructionBin = "01000001000000100000000000000000",
                InstructionID = 228,
                MemoryID = MemoryMap.COP0,
                Name = "BC0FL",
                Syntax = "BC0FL offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000000000000000",
                ArgumentInfo = "6:COP0;5:BC0;5:BC0T",
                Info = "Branch on Coprocessor 0 True",
                InstructionBin = "01000001000000010000000000000000",
                InstructionID = 229,
                MemoryID = MemoryMap.COP0,
                Name = "BC0T",
                Syntax = "BC0T offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000000000000000",
                ArgumentInfo = "6:COP0;5:BC0;5:BC0TL",
                Info = "Branch on Coprocessor 0 True Likely",
                InstructionBin = "01000001000000110000000000000000",
                InstructionID = 230,
                MemoryID = MemoryMap.COP0,
                Name = "BC0TL",
                Syntax = "BC0TL offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111111111111111111111",
                ArgumentInfo = "6:COP0;5:CO;15:0",
                Info = "Disable Interrupt",
                InstructionBin = "01000010000000000000000000111001",
                InstructionID = 231,
                MemoryID = MemoryMap.COP0,
                Name = "DI",
                Syntax = "DI"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111111111111111111111",
                ArgumentInfo = "6:COP0;5:CO;15:0",
                Info = "Enable Interrupt",
                InstructionBin = "01000010000000000000000000111000",
                InstructionID = 232,
                MemoryID = MemoryMap.COP0,
                Name = "EI",
                Syntax = "EI"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111111111111111111111",
                ArgumentInfo = "6:COP0;5:CO;15:0",
                Info = "Exception Return",
                InstructionBin = "01000010000000000000000000011000",
                InstructionID = 233,
                MemoryID = MemoryMap.COP0,
                Name = "ERET",
                Syntax = "ERET"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MF0;5:rt;5:11000",
                Info = "Move from Breakpoint Control Register",
                InstructionBin = "01000000000000001100000000000000",
                InstructionID = 234,
                MemoryID = MemoryMap.COP0,
                Name = "MFBPC",
                Syntax = "MFBPC rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP0;5:MF0;5:rt;5:c0reg",
                Info = "Move from System Control Coprocessor",
                InstructionBin = "01000000000000000000000000000000",
                InstructionID = 235,
                MemoryID = MemoryMap.COP0,
                Name = "MFC0",
                Syntax = "MFC0 rt, c0reg"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MF0;5:rt;5:11000",
                Info = "Move from Data Address Breakpoint Register",
                InstructionBin = "01000000000000001100000000000100",
                InstructionID = 236,
                MemoryID = MemoryMap.COP0,
                Name = "MFDAB",
                Syntax = "MFDAB rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MF0;5:rt;5:11000",
                Info = "Move from Data Address Breakpoint Mask Register",
                InstructionBin = "01000000000000001100000000000101",
                InstructionID = 237,
                MemoryID = MemoryMap.COP0,
                Name = "MFDABM",
                Syntax = "MFDABM rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MF0;5:rt;5:11000",
                Info = "Move from Data value Breakpoint Register",
                InstructionBin = "01000000000000001100000000000110",
                InstructionID = 238,
                MemoryID = MemoryMap.COP0,
                Name = "MFDVB",
                Syntax = "MFDVB rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MF0;5:rt;5:11000",
                Info = "Move from Data Value Breakpoint Mask Register",
                InstructionBin = "01000000000000001100000000000111",
                InstructionID = 239,
                MemoryID = MemoryMap.COP0,
                Name = "MFDVBM",
                Syntax = "MFDVBM rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MF0;5:rt;5:11000",
                Info = "Move from Instruction Address Breakpoint Register",
                InstructionBin = "01000000000000001100000000000010",
                InstructionID = 240,
                MemoryID = MemoryMap.COP0,
                Name = "MFIAB",
                Syntax = "MFIAB rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MF0;5:rt;5:11000",
                Info = "Move from Instruction Address Breakpoint Mask Register",
                InstructionBin = "01000000000000001100000000000011",
                InstructionID = 241,
                MemoryID = MemoryMap.COP0,
                Name = "MFIABM",
                Syntax = "MFIABM rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111000001",
                ArgumentInfo = "6:COP0;5:MF0;5:rt;5:11001;5:0;5:reg",
                Info = "Move from Performance Counter",
                InstructionBin = "01000000000000001100100000000001",
                InstructionID = 242,
                MemoryID = MemoryMap.COP0,
                Name = "MFPC",
                Syntax = "MFPC rt, reg"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111000001",
                ArgumentInfo = "6:COP0;5:MF0;5:rt;5:11001;5:0;5:reg",
                Info = "Move from Performance Event Specifier",
                InstructionBin = "01000000000000001100100000000000",
                InstructionID = 243,
                MemoryID = MemoryMap.COP0,
                Name = "MFPS",
                Syntax = "MFPS rt, reg"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MT0;5:rt;5:11000",
                Info = "Move to Breakpoint Control Register",
                InstructionBin = "01000000100000001100000000000000",
                InstructionID = 244,
                MemoryID = MemoryMap.COP0,
                Name = "MTBPC",
                Syntax = "MTBPC rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP0;5:MT0;5:rt;5:c0reg",
                Info = "Move to System Control Coprocessor",
                InstructionBin = "01000000100000000000000000000000",
                InstructionID = 245,
                MemoryID = MemoryMap.COP0,
                Name = "MTC0",
                Syntax = "MTC0 rt, c0reg"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MT0;5:rt;5:11000",
                Info = "Move to Data Address Breakpoint Register",
                InstructionBin = "01000000100000001100000000000100",
                InstructionID = 246,
                MemoryID = MemoryMap.COP0,
                Name = "MTDAB",
                Syntax = "MTDAB rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MT0;5:rt;5:11000",
                Info = "Move to Data Address Breakpoint Mask Register",
                InstructionBin = "01000000100000001100000000000101",
                InstructionID = 247,
                MemoryID = MemoryMap.COP0,
                Name = "MTDABM",
                Syntax = "MTDABM rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MT0;5:rt;5:11000",
                Info = "Move to Data Value Breakpoint Register",
                InstructionBin = "01000000100000001100000000000110",
                InstructionID = 248,
                MemoryID = MemoryMap.COP0,
                Name = "MTDVB",
                Syntax = "MTDVB rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MT0;5:rt;5:11000",
                Info = "Move to Data Value Breakpoint Mask Register",
                InstructionBin = "01000000100000001100000000000111",
                InstructionID = 249,
                MemoryID = MemoryMap.COP0,
                Name = "MTDVBM",
                Syntax = "MTDVBM rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MT0;5:rt;5:11000",
                Info = "Move to Instruction Address Breakpoint Register",
                InstructionBin = "01000000100000001100000000000010",
                InstructionID = 250,
                MemoryID = MemoryMap.COP0,
                Name = "MTIAB",
                Syntax = "MTIAB rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MT0;5:rt;5:11000",
                Info = "Move to Instruction Address Breakpoint Register",
                InstructionBin = "01000000100000001100000000000010",
                InstructionID = 251,
                MemoryID = MemoryMap.COP0,
                Name = "MTIAB",
                Syntax = "Function"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111111111",
                ArgumentInfo = "6:COP0;5:MT0;5:rt;5:11000",
                Info = "Move to Instruction Address Breakpoint Mask Register",
                InstructionBin = "01000000100000001100000000000011",
                InstructionID = 252,
                MemoryID = MemoryMap.COP0,
                Name = "MTIABM",
                Syntax = "MTIABM rt"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111000001",
                ArgumentInfo = "6:COP0;5:MT0;5:rt;5:11001;5:0;5:reg",
                Info = "Move to Performance Counter",
                InstructionBin = "01000000100000001100100000000001",
                InstructionID = 253,
                MemoryID = MemoryMap.COP0,
                Name = "MTPC",
                Syntax = "MTPC rt, reg"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111111111000001",
                ArgumentInfo = "6:COP0;5:MT0;5:rt;5:11001;5:0;5:reg",
                Info = "Move to Performance Event Specifier",
                InstructionBin = "01000000100000001100100000000000",
                InstructionID = 254,
                MemoryID = MemoryMap.COP0,
                Name = "MTPS",
                Syntax = "MTPS rt, reg"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000000000111111",
                ArgumentInfo = "6:COP1;5:S;5:0;5:fs;5:fd",
                Info = "Floating Point Absolute Value",
                InstructionBin = "01000110000000000000000000000101",
                InstructionID = 255,
                MemoryID = MemoryMap.COP1,
                Name = "ABS.S",
                Syntax = "ABS.S fd, fs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:fd",
                Info = "Floating Point ADD",
                InstructionBin = "01000110000000000000000000000000",
                InstructionID = 256,
                MemoryID = MemoryMap.COP1,
                Name = "ADD.S",
                Syntax = "ADD.S fd, fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:0",
                Info = "Floating Point Add to Accumulator",
                InstructionBin = "01000110000000000000000000011000",
                InstructionID = 257,
                MemoryID = MemoryMap.COP1,
                Name = "ADDA.S",
                Syntax = "ADDA.S fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000000000000000",
                ArgumentInfo = "6:COP1;5:BC1;5:BC1F",
                Info = "Branch on FP False",
                InstructionBin = "01000101000000000000000000000000",
                InstructionID = 258,
                MemoryID = MemoryMap.COP1,
                Name = "BC1F",
                Syntax = "BC1F offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000000000000000",
                ArgumentInfo = "6:COP1;5:BC1;5:BC1FL",
                Info = "Branch on FP False Likely",
                InstructionBin = "01000101000000100000000000000000",
                InstructionID = 259,
                MemoryID = MemoryMap.COP1,
                Name = "BC1FL",
                Syntax = "BC1FL offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000000000000000",
                ArgumentInfo = "6:COP1;5:BC1;5:BC1T",
                Info = "Branch on FP True",
                InstructionBin = "01000101000000010000000000000000",
                InstructionID = 260,
                MemoryID = MemoryMap.COP1,
                Name = "BC1T",
                Syntax = "BC1T offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000000000000000",
                ArgumentInfo = "6:COP1;5:BC1;5:BC1TL",
                Info = "Branch on FP True Likely",
                InstructionBin = "01000101000000110000000000000000",
                InstructionID = 261,
                MemoryID = MemoryMap.COP1,
                Name = "BC1TL",
                Syntax = "BC1TL offset"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:0;2:FC;1:0;2:cond",
                Info = "Floating Point Compare",
                InstructionBin = "01000110000000000000000000110010",
                InstructionID = 262,
                MemoryID = MemoryMap.COP1,
                Name = "C.EQ.S",
                Syntax = "C.EQ.S fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:0;2:FC;1:0;2:cond",
                Info = "Floating Point Compare",
                InstructionBin = "01000110000000000000000000110000",
                InstructionID = 263,
                MemoryID = MemoryMap.COP1,
                Name = "C.F.S",
                Syntax = "C.F.S fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:0;2:FC;1:0;2:cond",
                Info = "Floating Point Compare",
                InstructionBin = "01000110000000000000000000110110",
                InstructionID = 264,
                MemoryID = MemoryMap.COP1,
                Name = "C.LE.S",
                Syntax = "C.LE.S fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:0;2:FC;1:0;2:cond",
                Info = "Floating Point Compare",
                InstructionBin = "01000110000000000000000000110100",
                InstructionID = 265,
                MemoryID = MemoryMap.COP1,
                Name = "C.LT.S",
                Syntax = "C.LT.S fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP1;5:CFC1;5:rt;5:fs",
                Info = "Move Control Word from Floating Point",
                InstructionBin = "01000100010000000000000000000000",
                InstructionID = 266,
                MemoryID = MemoryMap.COP1,
                Name = "CFC1",
                Syntax = "CFC1 rt, fs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP1;5:CTC1;5:rt;5:fs",
                Info = "Move Control Word to Floating Point",
                InstructionBin = "01000100110000000000000000000000",
                InstructionID = 267,
                MemoryID = MemoryMap.COP1,
                Name = "CTC1",
                Syntax = "CTC1 rt, fs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000000000111111",
                ArgumentInfo = "6:COP1;5:W;5:0;5:fs;5:fd",
                Info = "Fixed-point Convert to Single Floating Point",
                InstructionBin = "01000110100000000000000000100000",
                InstructionID = 268,
                MemoryID = MemoryMap.COP1,
                Name = "CVT.S.W",
                Syntax = "CVT.S.W fd, fs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000000000111111",
                ArgumentInfo = "6:COP1;5:S;5:0;5:fs;5:fd",
                Info = "Floating Point Convert to Word Fixed-point",
                InstructionBin = "01000110000000000000000000100100",
                InstructionID = 269,
                MemoryID = MemoryMap.COP1,
                Name = "CVT.W.S",
                Syntax = "CVT.W.S fd, fs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:fd",
                Info = "Floating Point Divide",
                InstructionBin = "01000110000000000000000000000011",
                InstructionID = 270,
                MemoryID = MemoryMap.COP1,
                Name = "DIV.S",
                Syntax = "DIV.S fd, fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:LWC1;5:base;5:ft",
                Info = "Load Word to Floating Point",
                InstructionBin = "11000100000000000000000000000000",
                InstructionID = 271,
                MemoryID = MemoryMap.COP1,
                Name = "LWC1",
                Syntax = "LWC1 ft, offset(base)"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:fd",
                Info = "Floating Point Multiply-ADD",
                InstructionBin = "01000110000000000000000000011100",
                InstructionID = 272,
                MemoryID = MemoryMap.COP1,
                Name = "MADD.S",
                Syntax = "MADD.S fd, fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:0",
                Info = "Floating Point Multiply-Add",
                InstructionBin = "01000110000000000000000000011110",
                InstructionID = 273,
                MemoryID = MemoryMap.COP1,
                Name = "MADDA.S",
                Syntax = "MADDA.S fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:fd",
                Info = "Floating Point Maximum",
                InstructionBin = "01000110000000000000000000101000",
                InstructionID = 274,
                MemoryID = MemoryMap.COP1,
                Name = "MAX.S",
                Syntax = "MAX.S fd, fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP1;5:MFC1;5:rt;5:fs",
                Info = "Move Word from Floating Point",
                InstructionBin = "01000100000000000000000000000000",
                InstructionID = 275,
                MemoryID = MemoryMap.COP1,
                Name = "MFC1",
                Syntax = "MFC1 rt, fs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:fd",
                Info = "Floating Point Minimum",
                InstructionBin = "01000110000000000000000000110000",
                InstructionID = 276,
                MemoryID = MemoryMap.COP1,
                Name = "MIN.S",
                Syntax = "MIN.S fd, fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000000000111111",
                ArgumentInfo = "6:COP1;5:S;5:0;5:fs;5:fd",
                Info = "Floating Point Move",
                InstructionBin = "01000110000000000000000000000110",
                InstructionID = 277,
                MemoryID = MemoryMap.COP1,
                Name = "MOV.S",
                Syntax = "MOV.S fd, fs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:fd",
                Info = "Floating Point Multiply and Subtract",
                InstructionBin = "01000110000000000000000000011101",
                InstructionID = 278,
                MemoryID = MemoryMap.COP1,
                Name = "MSUB.S",
                Syntax = "MSUB.S fd, fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:0",
                Info = "Floating Point Multiply and Subtract from Accumulator",
                InstructionBin = "01000110000000000000000000011111",
                InstructionID = 279,
                MemoryID = MemoryMap.COP1,
                Name = "MSUBA.S",
                Syntax = "MSUBA.S fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP1;5:MTC1;5:rt;5:fs",
                Info = "Move Word to Floating Point",
                InstructionBin = "01000100100000000000000000000000",
                InstructionID = 280,
                MemoryID = MemoryMap.COP1,
                Name = "MTC1",
                Syntax = "MTC1 rt, fs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:fd",
                Info = "Floating Point Multiply",
                InstructionBin = "01000110000000000000000000000010",
                InstructionID = 281,
                MemoryID = MemoryMap.COP1,
                Name = "MUL.S",
                Syntax = "MUL.S fd, fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:0",
                Info = "Floating Point Multiply to Accumulator",
                InstructionBin = "01000110000000000000000000011010",
                InstructionID = 282,
                MemoryID = MemoryMap.COP1,
                Name = "MULA.S",
                Syntax = "MULA.S fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111111110000000000111111",
                ArgumentInfo = "6:COP1;5:S;5:0;5:fs;5:fd",
                Info = "Floating Point Negate",
                InstructionBin = "01000110000000000000000000000111",
                InstructionID = 283,
                MemoryID = MemoryMap.COP1,
                Name = "NEG.S",
                Syntax = "NEG.S fd, fs"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:fd",
                Info = "Floating Point Reciprocal Square Root",
                InstructionBin = "01000110000000000000000000010110",
                InstructionID = 284,
                MemoryID = MemoryMap.COP1,
                Name = "RSQRT.S",
                Syntax = "RSQRT.S fd, fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000001111100000111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:0;5:fd",
                Info = "Floating Point Square Root",
                InstructionBin = "01000110000000000000000000000100",
                InstructionID = 285,
                MemoryID = MemoryMap.COP1,
                Name = "SQRT.S",
                Syntax = "SQRT.S fd, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000000000111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:fd",
                Info = "Floating Point Subtract",
                InstructionBin = "01000110000000000000000000000001",
                InstructionID = 286,
                MemoryID = MemoryMap.COP1,
                Name = "SUB.S",
                Syntax = "SUB.S fd, fs, ft"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111111111000000000011111111111",
                ArgumentInfo = "6:COP1;5:S;5:ft;5:fs;5:0",
                Info = "Floating Point Subtract to Accumulator",
                InstructionBin = "01000110000000000000000000011001",
                InstructionID = 287,
                MemoryID = MemoryMap.COP1,
                Name = "SUBA.S",
                Syntax = "SUBA.S fs, ft I"
            });

            Instructions.Add(new Instruction()
            {
                ArgumentBin = "11111100000000000000000000000000",
                ArgumentInfo = "6:SWC1;5:base;5:ft",
                Info = "Store Word from Floating Point",
                InstructionBin = "11100100000000000000000000000000",
                InstructionID = 288,
                MemoryID = MemoryMap.COP1,
                Name = "SWC1",
                Syntax = "SWC1 ft, offset(base)"
            });

            Registers = new List<Register>();

            Registers.Add(new Register()
            {
                Bin = "00000",
                Hex = "0000",
                Info = "This register will result to always being zero.",
                MemoryID = MemoryMap.EE,
                Name = "zero",
                RegisterID = 1
            });

            Registers.Add(new Register()
            {
                Bin = "00001",
                Hex = "0001",
                Info = "This register is set aside for the assembler.",
                MemoryID = MemoryMap.EE,
                Name = "at",
                RegisterID = 2
            });

            Registers.Add(new Register()
            {
                Bin = "00010",
                Hex = "0002",
                Info = "This register is used as a return value.",
                MemoryID = MemoryMap.EE,
                Name = "v0",
                RegisterID = 3
            });

            Registers.Add(new Register()
            {
                Bin = "00011",
                Hex = "0003",
                Info = "This register is used as a return value.",
                MemoryID = MemoryMap.EE,
                Name = "v1",
                RegisterID = 4
            });

            Registers.Add(new Register()
            {
                Bin = "00100",
                Hex = "0004",
                Info = "This register is used as an Arguement.",
                MemoryID = MemoryMap.EE,
                Name = "a0",
                RegisterID = 5
            });

            Registers.Add(new Register()
            {
                Bin = "00101",
                Hex = "0005",
                Info = "This register is used as an Arguement.",
                MemoryID = MemoryMap.EE,
                Name = "a1",
                RegisterID = 6
            });

            Registers.Add(new Register()
            {
                Bin = "00110",
                Hex = "0006",
                Info = "This register is used as an Arguement.",
                MemoryID = MemoryMap.EE,
                Name = "a2",
                RegisterID = 7
            });

            Registers.Add(new Register()
            {
                Bin = "00111",
                Hex = "0007",
                Info = "This register is used as an Arguement.",
                MemoryID = MemoryMap.EE,
                Name = "a3",
                RegisterID = 8
            });

            Registers.Add(new Register()
            {
                Bin = "01000",
                Hex = "0008",
                Info = "This register is used as temporary storage.",
                MemoryID = MemoryMap.EE,
                Name = "t0",
                RegisterID = 9
            });

            Registers.Add(new Register()
            {
                Bin = "01001",
                Hex = "0009",
                Info = "This register is used as temporary storage.",
                MemoryID = MemoryMap.EE,
                Name = "t1",
                RegisterID = 10
            });

            Registers.Add(new Register()
            {
                Bin = "01010",
                Hex = "000a",
                Info = "This register is used as temporary storage.",
                MemoryID = MemoryMap.EE,
                Name = "t2",
                RegisterID = 11
            });

            Registers.Add(new Register()
            {
                Bin = "01011",
                Hex = "000b",
                Info = "This register is used as temporary storage.",
                MemoryID = MemoryMap.EE,
                Name = "t3",
                RegisterID = 12
            });

            Registers.Add(new Register()
            {
                Bin = "01100",
                Hex = "000c",
                Info = "This register is used as temporary storage.",
                MemoryID = MemoryMap.EE,
                Name = "t4",
                RegisterID = 13
            });

            Registers.Add(new Register()
            {
                Bin = "01101",
                Hex = "000d",
                Info = "This register is used as temporary storage.",
                MemoryID = MemoryMap.EE,
                Name = "t5",
                RegisterID = 14
            });

            Registers.Add(new Register()
            {
                Bin = "01110",
                Hex = "000e",
                Info = "This register is used as temporary storage.",
                MemoryID = MemoryMap.EE,
                Name = "t6",
                RegisterID = 15
            });

            Registers.Add(new Register()
            {
                Bin = "01111",
                Hex = "000f",
                Info = "This register is used as temporary storage.",
                MemoryID = MemoryMap.EE,
                Name = "t7",
                RegisterID = 16
            });

            Registers.Add(new Register()
            {
                Bin = "10000",
                Hex = "0010",
                Info = "This register is used as saved storage.",
                MemoryID = MemoryMap.EE,
                Name = "s0",
                RegisterID = 17
            });

            Registers.Add(new Register()
            {
                Bin = "10001",
                Hex = "0011",
                Info = "This register is used as saved storage.",
                MemoryID = MemoryMap.EE,
                Name = "s1",
                RegisterID = 18
            });

            Registers.Add(new Register()
            {
                Bin = "10010",
                Hex = "0012",
                Info = "This register is used as saved storage.",
                MemoryID = MemoryMap.EE,
                Name = "s2",
                RegisterID = 19
            });

            Registers.Add(new Register()
            {
                Bin = "10011",
                Hex = "0013",
                Info = "This register is used as saved storage.",
                MemoryID = MemoryMap.EE,
                Name = "s3",
                RegisterID = 20
            });

            Registers.Add(new Register()
            {
                Bin = "10100",
                Hex = "0014",
                Info = "This register is used as saved storage.",
                MemoryID = MemoryMap.EE,
                Name = "s4",
                RegisterID = 21
            });

            Registers.Add(new Register()
            {
                Bin = "10101",
                Hex = "0015",
                Info = "This register is used as saved storage.",
                MemoryID = MemoryMap.EE,
                Name = "s5",
                RegisterID = 22
            });

            Registers.Add(new Register()
            {
                Bin = "10110",
                Hex = "0016",
                Info = "This register is used as saved storage.",
                MemoryID = MemoryMap.EE,
                Name = "s6",
                RegisterID = 23
            });

            Registers.Add(new Register()
            {
                Bin = "10111",
                Hex = "0017",
                Info = "This register is used as saved storage.",
                MemoryID = MemoryMap.EE,
                Name = "s7",
                RegisterID = 24
            });

            Registers.Add(new Register()
            {
                Bin = "11000",
                Hex = "0018",
                Info = "This register is used as temporary storage.",
                MemoryID = MemoryMap.EE,
                Name = "t8",
                RegisterID = 25
            });

            Registers.Add(new Register()
            {
                Bin = "11001",
                Hex = "0019",
                Info = "This register is used as temporary storage.",
                MemoryID = MemoryMap.EE,
                Name = "t9",
                RegisterID = 26
            });

            Registers.Add(new Register()
            {
                Bin = "11010",
                Hex = "001a",
                Info = "This register is used as a kernal register.",
                MemoryID = MemoryMap.EE,
                Name = "k0",
                RegisterID = 27
            });

            Registers.Add(new Register()
            {
                Bin = "11011",
                Hex = "001b",
                Info = "This register is used as a kernal register.",
                MemoryID = MemoryMap.EE,
                Name = "k1",
                RegisterID = 28
            });

            Registers.Add(new Register()
            {
                Bin = "11100",
                Hex = "001c",
                Info = "This register is used as a global pointer.",
                MemoryID = MemoryMap.EE,
                Name = "gp",
                RegisterID = 29
            });

            Registers.Add(new Register()
            {
                Bin = "11101",
                Hex = "001d",
                Info = "This register is used as a stack pointer.",
                MemoryID = MemoryMap.EE,
                Name = "sp",
                RegisterID = 30
            });

            Registers.Add(new Register()
            {
                Bin = "11110",
                Hex = "001e",
                Info = "This register is used as a frame pointer.",
                MemoryID = MemoryMap.EE,
                Name = "fp",
                RegisterID = 31
            });

            Registers.Add(new Register()
            {
                Bin = "11111",
                Hex = "001f",
                Info = "This register is used for storage of a return address.",
                MemoryID = MemoryMap.EE,
                Name = "ra",
                RegisterID = 32
            });

            Registers.Add(new Register()
            {
                Bin = "00000",
                Hex = "0000",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "index",
                RegisterID = 33
            });

            Registers.Add(new Register()
            {
                Bin = "00001",
                Hex = "0001",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "random",
                RegisterID = 34
            });

            Registers.Add(new Register()
            {
                Bin = "00010",
                Hex = "0002",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "entrylo0",
                RegisterID = 35
            });

            Registers.Add(new Register()
            {
                Bin = "00011",
                Hex = "0003",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "entryo1",
                RegisterID = 36
            });

            Registers.Add(new Register()
            {
                Bin = "00100",
                Hex = "0004",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "context",
                RegisterID = 37
            });

            Registers.Add(new Register()
            {
                Bin = "00101",
                Hex = "0005",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "pagemask",
                RegisterID = 38
            });

            Registers.Add(new Register()
            {
                Bin = "00110",
                Hex = "0006",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "wired",
                RegisterID = 39
            });

            Registers.Add(new Register()
            {
                Bin = "00111",
                Hex = "0007",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "7",
                RegisterID = 40
            });

            Registers.Add(new Register()
            {
                Bin = "01000",
                Hex = "0008",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "badvaddr",
                RegisterID = 41
            });

            Registers.Add(new Register()
            {
                Bin = "01001",
                Hex = "0009",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "count",
                RegisterID = 42
            });

            Registers.Add(new Register()
            {
                Bin = "01010",
                Hex = "000a",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "entryhi",
                RegisterID = 43
            });

            Registers.Add(new Register()
            {
                Bin = "01011",
                Hex = "000b",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "compare",
                RegisterID = 44
            });

            Registers.Add(new Register()
            {
                Bin = "01100",
                Hex = "000c",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "status",
                RegisterID = 45
            });

            Registers.Add(new Register()
            {
                Bin = "01101",
                Hex = "000d",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "cause",
                RegisterID = 46
            });

            Registers.Add(new Register()
            {
                Bin = "01110",
                Hex = "000e",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "epc",
                RegisterID = 47
            });

            Registers.Add(new Register()
            {
                Bin = "01111",
                Hex = "000f",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "prid",
                RegisterID = 48
            });

            Registers.Add(new Register()
            {
                Bin = "10000",
                Hex = "0010",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "config",
                RegisterID = 49
            });

            Registers.Add(new Register()
            {
                Bin = "10001",
                Hex = "0011",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "lladr",
                RegisterID = 50
            });

            Registers.Add(new Register()
            {
                Bin = "10010",
                Hex = "0012",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "watchlo",
                RegisterID = 51
            });

            Registers.Add(new Register()
            {
                Bin = "10011",
                Hex = "0013",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "watchhi",
                RegisterID = 52
            });

            Registers.Add(new Register()
            {
                Bin = "10100",
                Hex = "0014",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "xcontext",
                RegisterID = 53
            });

            Registers.Add(new Register()
            {
                Bin = "10101",
                Hex = "0015",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "21",
                RegisterID = 54
            });

            Registers.Add(new Register()
            {
                Bin = "10110",
                Hex = "0016",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "22",
                RegisterID = 55
            });

            Registers.Add(new Register()
            {
                Bin = "10111",
                Hex = "0017",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "badpaddr",
                RegisterID = 56
            });

            Registers.Add(new Register()
            {
                Bin = "11000",
                Hex = "0018",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "debug",
                RegisterID = 57
            });

            Registers.Add(new Register()
            {
                Bin = "11001",
                Hex = "0019",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "perf",
                RegisterID = 58
            });

            Registers.Add(new Register()
            {
                Bin = "11010",
                Hex = "001a",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "ecc",
                RegisterID = 59
            });

            Registers.Add(new Register()
            {
                Bin = "11011",
                Hex = "001b",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "cacheerr",
                RegisterID = 60
            });

            Registers.Add(new Register()
            {
                Bin = "11100",
                Hex = "001c",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "taglo",
                RegisterID = 61
            });

            Registers.Add(new Register()
            {
                Bin = "11101",
                Hex = "001d",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "taghi",
                RegisterID = 62
            });

            Registers.Add(new Register()
            {
                Bin = "11110",
                Hex = "001e",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "errepc",
                RegisterID = 63
            });

            Registers.Add(new Register()
            {
                Bin = "11111",
                Hex = "001f",
                Info = "None",
                MemoryID = MemoryMap.COP0,
                Name = "31",
                RegisterID = 64
            });

            Registers.Add(new Register()
            {
                Bin = "00000",
                Hex = "0000",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f0",
                RegisterID = 65
            });

            Registers.Add(new Register()
            {
                Bin = "00001",
                Hex = "0001",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f1",
                RegisterID = 66
            });

            Registers.Add(new Register()
            {
                Bin = "00010",
                Hex = "0002",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f2",
                RegisterID = 67
            });

            Registers.Add(new Register()
            {
                Bin = "00011",
                Hex = "0003",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f3",
                RegisterID = 68
            });

            Registers.Add(new Register()
            {
                Bin = "00100",
                Hex = "0004",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f4",
                RegisterID = 69
            });

            Registers.Add(new Register()
            {
                Bin = "00101",
                Hex = "0005",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f5",
                RegisterID = 70
            });

            Registers.Add(new Register()
            {
                Bin = "00110",
                Hex = "0006",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f6",
                RegisterID = 71
            });

            Registers.Add(new Register()
            {
                Bin = "00111",
                Hex = "0007",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f7",
                RegisterID = 72
            });

            Registers.Add(new Register()
            {
                Bin = "01000",
                Hex = "0008",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f8",
                RegisterID = 73
            });

            Registers.Add(new Register()
            {
                Bin = "01001",
                Hex = "0009",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f9",
                RegisterID = 74
            });

            Registers.Add(new Register()
            {
                Bin = "01010",
                Hex = "000a",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f10",
                RegisterID = 75
            });

            Registers.Add(new Register()
            {
                Bin = "01011",
                Hex = "000b",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f11",
                RegisterID = 76
            });

            Registers.Add(new Register()
            {
                Bin = "01100",
                Hex = "000c",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f12",
                RegisterID = 77
            });

            Registers.Add(new Register()
            {
                Bin = "01101",
                Hex = "000d",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f13",
                RegisterID = 78
            });

            Registers.Add(new Register()
            {
                Bin = "01110",
                Hex = "000e",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f14",
                RegisterID = 79
            });

            Registers.Add(new Register()
            {
                Bin = "01111",
                Hex = "000f",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f15",
                RegisterID = 80
            });

            Registers.Add(new Register()
            {
                Bin = "10000",
                Hex = "0010",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f16",
                RegisterID = 81
            });

            Registers.Add(new Register()
            {
                Bin = "10001",
                Hex = "0011",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f17",
                RegisterID = 82
            });

            Registers.Add(new Register()
            {
                Bin = "10010",
                Hex = "0012",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f18",
                RegisterID = 83
            });

            Registers.Add(new Register()
            {
                Bin = "10011",
                Hex = "0013",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f19",
                RegisterID = 84
            });

            Registers.Add(new Register()
            {
                Bin = "10100",
                Hex = "0014",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f20",
                RegisterID = 85
            });

            Registers.Add(new Register()
            {
                Bin = "10101",
                Hex = "0015",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f21",
                RegisterID = 86
            });

            Registers.Add(new Register()
            {
                Bin = "10110",
                Hex = "0016",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f22",
                RegisterID = 87
            });

            Registers.Add(new Register()
            {
                Bin = "10111",
                Hex = "0017",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f23",
                RegisterID = 88
            });

            Registers.Add(new Register()
            {
                Bin = "11000",
                Hex = "0018",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f24",
                RegisterID = 89
            });

            Registers.Add(new Register()
            {
                Bin = "11001",
                Hex = "0019",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f25",
                RegisterID = 90
            });

            Registers.Add(new Register()
            {
                Bin = "11010",
                Hex = "001a",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f26",
                RegisterID = 91
            });

            Registers.Add(new Register()
            {
                Bin = "11011",
                Hex = "001b",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f27",
                RegisterID = 92
            });

            Registers.Add(new Register()
            {
                Bin = "11100",
                Hex = "001c",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f28",
                RegisterID = 93
            });

            Registers.Add(new Register()
            {
                Bin = "11101",
                Hex = "001d",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f29",
                RegisterID = 94
            });

            Registers.Add(new Register()
            {
                Bin = "11110",
                Hex = "001e",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f30",
                RegisterID = 95
            });

            Registers.Add(new Register()
            {
                Bin = "11111",
                Hex = "001f",
                Info = "This register is reserved for floating point numbers.",
                MemoryID = MemoryMap.COP1,
                Name = "$f31",
                RegisterID = 96
            });

            Assembler = new _Assembler()
            {
                Instructions = Instructions,
                Registers = Registers
            };

            Disassembler = new _Disassembler()
            {
                Instructions = Instructions,
                Registers = Registers
            };
        }

        public string Assemble(string source) => Assembler.Assemble(source);
        public string Disassemble(string code) => Disassembler.Disassemble(code);

        private class _Assembler
        {
            public List<Instruction> Instructions { get; set; }

            public List<Register> Registers { get; set; }

            private Register FindRegisterByName(string name) => Registers.FirstOrDefault(x => x.Name.Equals(name));

            public List<Instruction> FindInstructionByName(string name) => Instructions.Where(x => x.Name.ToLower().Equals(name)).ToList();

            private string FormatRegister(string bin, int binIndex, string arg, string[] rule) => Placeholders.EERegister.Contains(rule[1])
                || Placeholders.COP0Register.Contains(rule[1])
                || Placeholders.COP1Register.Contains(rule[1])
                    ? Helper.InsertBin(binIndex, Convert.ToInt32(rule[0]), bin, FindRegisterByName(arg)?.Bin)
                    : bin;

            private string FormatImmediate(string bin, int binIndex, string operation, string syntax) => syntax.Contains(Placeholders.Branch) == false
                && syntax.Contains(Placeholders.Immediate[0])
                || syntax.Contains(Placeholders.Immediate[1]) 
                    ? Helper.InsertBin(binIndex, 16, bin, Helper.ZeroPad(Helper.HexToBin(Parse.WithRegex(operation, @"\$([a-f0-9]{4,8})"), 16), 16))
                    : bin;

            private string FormatBranch(string bin, int binIndex, string operation, string syntax)
            {
                if (syntax.Contains(Placeholders.Immediate[1]) == false && syntax.Contains(Placeholders.Branch))
                {
                    var offset = Convert.ToInt32(Parse.WithRegex(operation, @"\$([a-f0-9]{4})"), 16);
                    if (offset >= 32767)
                    {
                        var value = Convert.ToString(((65536 - offset) / 4) * -1, 2);
                        bin = Helper.InsertBin(binIndex, 16, bin, value.Substring(value.Count() - 16, 16));
                    }
                    else {
                        var value = Helper.ZeroPad(Convert.ToString((offset / 4) - 1, 2), 16);
                        bin = Helper.InsertBin(binIndex, 16, bin, value);
                    }
                }
                return bin;
            }

            private string FormatInteger(string bin, string operation, string syntax) => syntax.Contains(Placeholders.Integer)
                ? Helper.InsertBin(24, 8, bin, Helper.ZeroPad(Convert.ToString(Convert.ToInt32(Parse.WithRegex(operation, @" ([0-9]{1,2})")) * 64, 2), 8))
                : bin;

            private string FormatCode(string bin, int binIndex, string arg, string[] rule) => rule[1].Contains(Placeholders.Code)
                ? Helper.InsertBin(binIndex, 20, bin, Helper.ZeroPad(Convert.ToString(Convert.ToInt32(arg), 2), 20))
                : bin;

            private string FormatCall(string bin, int binIndex, string operation, string syntax) => syntax.Contains(Placeholders.Call)
                ? Helper.InsertBin(binIndex, 26, bin, Helper.ZeroPad(Convert.ToString(Convert.ToInt32(Parse.WithRegex(operation, @"\$([a-f0-9]{8})"), 16) / 4, 2), 26))
                : bin;
            
            public string PatchTargetLabels(string operation, List<string> syntaxArgs, int? target = null, int label = 0, string text = "")
            {
                if (target != null)
                {
                    if (syntaxArgs.Contains(Placeholders.Call))
                        operation = operation.Replace($":{text.ToLower()}", $"${Helper.ZeroPad(Convert.ToString(label, 16), 8)}");
                    else
                    {
                        var value = 0;
                        if (label > target)
                            value = (label - (int)target);
                        else
                            value = (((int)target - label) * -1) - 4;

                        var offset = Helper.ZeroPad(Convert.ToString(value, 16), 4);
                        operation = operation.Replace($":{text.ToLower()}", $"${offset.Substring(offset.Count() - 4, 4)}");
                    }
                }
                return operation;
            }

            public string Assemble(string operation)
            {
                operation = operation.ToLower();
                
                var instruction = FindInstructionByName(Helper.ParseInstructionName(operation)).FirstOrDefault();
                var operationArgs = Helper.ParseArgs(operation);
                var syntaxArgs = Helper.ParseArgs(instruction.Syntax);
                var binary = instruction.InstructionBin;
                var rules = Helper.SplitRules(instruction);
                var i = 0;
                var syntax = instruction.Syntax.ToLower();
                var ruleSize = 0;
                
                foreach (string[] rule in rules)
                {
                    ruleSize = Convert.ToInt32(rule[0]);
                    if (syntaxArgs.Contains(rule[1]))
                    {
                        binary = FormatRegister(binary, i, operationArgs[syntaxArgs.IndexOf(rule[1])], rule);
                        binary = FormatCode(binary, i, operationArgs[syntaxArgs.IndexOf(rule[1])], rule);
                    }
                    i += ruleSize;
                }

                binary = FormatBranch(binary, i, operation, syntax);
                binary = FormatImmediate(binary, i, operation, syntax);
                binary = FormatInteger(binary, operation, syntax);
                binary = FormatCall(binary, i, operation, syntax);

                return Helper.BinToHex(binary);
            }
        }

        private class _Disassembler
        {
            public List<Instruction> Instructions { get; set; }

            public List<Register> Registers { get; set; }

            private Register FindRegisterByBin(string bin, MemoryMap memoryID) => Registers.FirstOrDefault(x => x.Bin.Equals(bin) && x.MemoryID.Equals(memoryID));

            private Register FindRegisterByHex(string hex, MemoryMap memoryID) => Registers.FirstOrDefault(x => x.Hex.Equals(hex) && x.MemoryID.Equals(memoryID));

            private List<Instruction> FindInstructionByHex(string hex) => FindInstructionByBin(Helper.HexToBin(hex));

            private List<Instruction> FindInstructionByBin(string bin)
            {
                return Instructions.Where(x =>
                {
                    var oneMatchCount = 0;
                    var oneCount = x.ArgumentBin.Where(num => num.Equals('1')).Count();

                    for (var i = 0; i < 32; i++)
                        if (x.ArgumentBin[i].Equals('1') && x.InstructionBin[i].Equals(bin[i])) oneMatchCount++;

                    return oneMatchCount.Equals(oneCount) ? true : false;

                }).ToList();
            }

            private string FormatRegister(string syntax, string bin, int i, string rule, int length)
            {
                if (Placeholders.EERegister.Contains(rule))
                    return syntax.Replace(rule, FindRegisterByBin(bin.Substring(i, length), MemoryMap.EE).Name);

                else if (Placeholders.COP0Register.Equals(rule))
                    return syntax.Replace(rule, FindRegisterByBin(bin.Substring(i, length), MemoryMap.COP0).Name);

                else if (Placeholders.COP1Register.Contains(rule))
                    return syntax.Replace(rule, FindRegisterByBin(bin.Substring(i, length), MemoryMap.COP1).Name);
                else
                    return syntax;
            }

            private string FormatInteger(string syntax, string bin, int i, string rule, int length) => Placeholders.Integer.Equals(rule)
                ? syntax.Replace(rule, Helper.BinToHex(bin.Substring(i, length), 0))
                : syntax;

            private string FormatCode(string syntax, string bin, int i, string rule, int length) => Placeholders.Code.Equals(rule)
                ? syntax.Replace(rule, Convert.ToInt32(bin.Substring(i, length), 2).ToString().PadLeft(4, '0'))
                : syntax;

            private string FormatImmediate(string syntax, string bin, int i)
            {
                if (syntax.ToString().Contains(Placeholders.Immediate[0]))
                    return syntax.Replace(Placeholders.Immediate[0], "$" + Helper.BinToHex(bin.Substring(i, 16), 4));

                else if (syntax.ToString().Contains(Placeholders.Immediate[1]))
                    return syntax.Replace(Placeholders.Immediate[1].TrimEnd('('), "$" + Helper.BinToHex(bin.Substring(i, 16), 4));

                else
                    return syntax;
            }

            private string FormatBranch(string syntax, string bin, int i)
            {
                if (syntax.ToString().Contains(Placeholders.Branch))
                {
                    int offset = (Convert.ToInt32(bin.Substring(i, 16), 2));
                    if (offset >= 32767 && offset <= 65536)
                    {
                        var offsetBin = Convert.ToString(((offset - 65536) * 4), 16);
                        return syntax.Replace(Placeholders.Branch, "$" + offsetBin.Substring(offsetBin.Count() - 4, 4));
                    }
                    else
                        return syntax.Replace(Placeholders.Branch, "$" + Convert.ToString((offset * 4) + 4, 16).PadLeft(4, '0'));
                }
                return syntax;
            }

            private string FormatCall(string syntax, string bin, int i) => syntax.ToString().Contains(Placeholders.Call)
                ? syntax.Replace(Placeholders.Call, "$" + Convert.ToString(Convert.ToInt32(bin.Substring(i, 26), 2) * 4, 16).PadLeft(8, '0'))
                : syntax;

            public string Disassemble(string hex)
            {
                var binary = Helper.HexToBin(hex);
                var instructions = FindInstructionByBin(binary);
                var instructionCount = instructions.Count();

                if (instructionCount.Equals(0))
                    return "unknown";

                var instruction = instructions[0].Name.Equals("NOP")
                    || instructionCount.Equals(1)
                        ? instructions[0]
                        : instructions[1];

                var i = 0;

                var syntax = instruction.Syntax.ToLower();

                foreach (string[] rule in Helper.SplitRules(instruction))
                {
                    var ruleSize = Convert.ToInt32(rule[0]);
                    syntax = FormatRegister(syntax, binary, i, rule[1], ruleSize);
                    syntax = FormatInteger(syntax, binary, i, rule[1], ruleSize);
                    syntax = FormatCode(syntax, binary, i, rule[1], ruleSize);
                    i += ruleSize;
                }

                syntax = FormatImmediate(syntax, binary, i);
                syntax = FormatBranch(syntax, binary, i);
                syntax = FormatCall(syntax, binary, i);

                return syntax.ToString();
            }
        }

    }

    

}

