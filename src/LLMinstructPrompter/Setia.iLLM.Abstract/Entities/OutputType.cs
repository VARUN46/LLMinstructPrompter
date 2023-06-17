using System;
using System.Collections.Generic;
using System.Text;

namespace Setia.iLLM.Abstract.Entities
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
