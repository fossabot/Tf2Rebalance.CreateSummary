using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;

namespace Tf2Rebalance.CreateSummary.Tests
{
    [TestClass]
    public class ConverterTests
    {
        private IDictionary<string, List<ItemInfo>> _itemInfos;

        [TestInitialize]
        public void TestInitialize()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();

            _itemInfos = AlliedModsWiki.GetItemInfos();
        }

        [TestMethod]
        [DataRow("tf2rebalance_attributes.example.txt", "tf2rebalance_attributes.example_summary.txt")]
        [DataRow("higps.txt", "higps_summary.txt")]
        [DataRow("higps_withoutClasses.txt", "higps_withoutClasses_summary.txt")]
        public void TestMethod1(string inputFilename, string expectedOutputFilename)
        {
            string input = File.ReadAllText(inputFilename);
            string expectedOutput = File.ReadAllText(expectedOutputFilename);

            Converter converter = new Converter(_itemInfos);


            string output = converter.Execute(input);

            Assert.AreEqual(expectedOutput, output);
        }
    }
}
