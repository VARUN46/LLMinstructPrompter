using System;
using System.Collections.Generic;
using System.Text;

namespace LLMinstructPrompter.Abstract.Entities
{
    public class PromptableObject
    {
        public string UserPrompt { get; set; }
        public OutputRandomness OutputRandomnessType { get; set; }
    }
}
