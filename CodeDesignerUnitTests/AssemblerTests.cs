using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeDesigner;
using System.Linq;
using System.Collections.Generic;

namespace CodeDesignerUnitTests
{

    [TestClass]
    public class MipsUnitTests
    {
        public Mips32 Mips { get; set; }

        [TestInitialize]
        public void Init()
        {
            Mips = new Mips32();
        }

        [TestMethod]
        public void Assemble()
        {
            Assert.AreEqual("27bd0800", Mips.Assemble("addiu sp, sp, $0800"));
            Assert.AreEqual("3c040001", Mips.Assemble("lui a0, $0001"));
            Assert.AreEqual("ad080000", Mips.Assemble("sw t0, $0000(t0)"));
            Assert.AreEqual("000840c0", Mips.Assemble("sll t0, t0, 3"));
            Assert.AreEqual("0000008c", Mips.Assemble("syscall (0002)"));
            Assert.AreEqual("1000ffff", Mips.Assemble("beq zero, zero, $FFFC"));
            Assert.AreEqual("10000001", Mips.Assemble("beq zero, zero, $0008"));
            Assert.AreEqual("1c800001", Mips.Assemble("bgtz a0, $0008"));
            Assert.AreEqual("0c028000", Mips.Assemble("jal $000A0000"));
            Assert.AreEqual("03e00008", Mips.Assemble("jr ra"));
            //Assert.AreEqual("0040f809", Mips.Assemble("jalr v0,"));
            Assert.AreEqual("e60c0000", Mips.Assemble("swc1 $f12, $0000(s0)"));
            Assert.AreEqual("00000000", Mips.Assemble("nop"));
            Assert.AreEqual("40196800", Mips.Assemble("mfc0 t9, cause"));
        }

        [TestMethod]
        public void Disassemble()
        {
            Assert.AreEqual("addiu sp, sp, $0800", Mips.Disassemble("27bd0800"));
            Assert.AreEqual("lui a0, $0001", Mips.Disassemble("3c040001"));
            Assert.AreEqual("sw t0, $0000(t0)", Mips.Disassemble("ad080000"));
            Assert.AreEqual("sll t0, t0, 3", Mips.Disassemble("000840c0"));
            Assert.AreEqual("syscall (0002)", Mips.Disassemble("0000008c"));
            Assert.AreEqual("beq zero, zero, $fffc", Mips.Disassemble("1000FFFF"));
            Assert.AreEqual("beq zero, zero, $0008", Mips.Disassemble("10000001"));
            Assert.AreEqual("bgtz a0, $0008", Mips.Disassemble("1c800001"));
            Assert.AreEqual("jal $000a0000", Mips.Disassemble("0c028000"));
            Assert.AreEqual("jr ra", Mips.Disassemble("03e00008"));
            Assert.AreEqual("jalr v0", Mips.Disassemble("0040f809"));
            Assert.AreEqual("swc1 $f12, $0000(s0)", Mips.Disassemble("e60c0000"));
            Assert.AreEqual("nop", Mips.Disassemble("00000000"));
            Assert.AreEqual("mfc0 t9, cause", Mips.Disassemble("40196800"));
        }

    }
}
