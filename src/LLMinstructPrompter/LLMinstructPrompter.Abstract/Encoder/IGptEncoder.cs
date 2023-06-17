using System;
using System.Collections.Generic;
using System.Text;

namespace LLMinstructPrompter.Abstract.Encoder
{
    public interface IGptEncoder
    {
        int[] Encode(string text);

        string Decode(int[] encodedData);
    }
}
