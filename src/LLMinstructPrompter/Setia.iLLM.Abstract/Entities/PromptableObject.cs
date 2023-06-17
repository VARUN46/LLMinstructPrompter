using System;
using System.Collections.Generic;
using System.Text;

namespace Setia.iLLM.Abstract.Entities
{
    public class PromptableObject
    {
        public string UserPrompt { get; set; }
        public string SystemPrompt { get; set; }
        public OutputRandomness OutputRandomnessType { get; set; }
    }
}
