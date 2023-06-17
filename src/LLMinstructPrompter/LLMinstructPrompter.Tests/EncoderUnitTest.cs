using LLMinstructPrompter.Abstract.Encoder;
using LLMinstructPrompter.Encoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setia.iLLM.Tests
{
    public class EncoderUnitTest
    {
        [Test]
        public void EncodeTest()
        {
            IGptEncoder gptEncoder = new GptEncoder();
            var textEncoding = gptEncoder.Encode("This is some text by Varun");
        }

        [Test]
        public void DecodeTest()
        {
            IGptEncoder gptEncoder = new GptEncoder();
            var textEncoding = gptEncoder.Encode("This is some text by Varun");
        }
    }
}
