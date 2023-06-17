using System;
using System.Collections.Generic;
using System.Text;

namespace Setia.iLLM.Abstract.Encoder
{
    public interface IGptEncoder
    {
        int[] Encode(string text);

        string Decode(int[] encodedData);
    }
}
