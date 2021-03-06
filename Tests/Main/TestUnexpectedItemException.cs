﻿using System.IO;
using System.Xml.Serialization;
using Tests.Lib;
using Xunit.Abstractions;

namespace Tests.Main
{
    /// <summary></summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for TestUnexpectedItemException
    public class TestUnexpectedItemException
    {
        private readonly ITestOutputHelper _helper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public TestUnexpectedItemException(ITestOutputHelper helper) { _helper = helper; }

        /// <summary>Tests the unexpected item exception set object data.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TestUnexpectedItemException_SetObjectData
        public void TestUnexpectedItemException_SetObjectData()
        {
            UnexpectedPropertyException e = new UnexpectedPropertyException("", null);
            XmlSerializer x = new XmlSerializer(e.GetType());
            var stringWriter = new StringWriter();
            x.Serialize(stringWriter, e);

            _helper.WriteLine(stringWriter.ToString());
        }
    }
}