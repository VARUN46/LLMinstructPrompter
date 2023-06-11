using System;
using System.Collections.Generic;
using System.Text;

namespace LLMinstructPrompter.Abstract.Entities
{
    public enum OutputType
    {
        Raw,
        JSON,
        CSV,
        BulletPoints,
        Default = Raw
    }
}
