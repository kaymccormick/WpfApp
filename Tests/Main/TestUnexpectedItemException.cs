using System.IO;
using System.Text;
using System.Xml.Serialization;
using Tests.Lib;
using Xunit.Abstractions;

namespace Tests.Main
{
    public class TestUnexpectedItemException
    {
        private readonly ITestOutputHelper _helper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public TestUnexpectedItemException(ITestOutputHelper helper) { _helper = helper; }

        public void TestUnexpectedItemException_SetObjectData()
        {
            UnexpectedPropertyException e = new UnexpectedPropertyException("", null);
            XmlSerializer x = new XmlSerializer(e.GetType());
            var stringBuilder = new StringBuilder();
            var stringWriter = new StringWriter();
            x.Serialize(stringWriter, e);

            _helper.WriteLine(stringWriter.ToString());
        }
    }
}