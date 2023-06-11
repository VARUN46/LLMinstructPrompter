using LLMinstructPrompter.Abstract.Entities;
using LLMinstructPrompter.Abstract.Prompts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LLMinstructPrompter.Prompts
{
    public class PromptDesigns : IPromptDesigns
    {
        private OutputType outputType;
        private OutputRandomness randomness;
        private Dictionary<int,PromptTask> promptTasks = new Dictionary<int,PromptTask>();

        public PromptableObject GetPrompt()
        {
            throw new NotImplementedException();
        }

        public IPromptDesigns SetOutputFormat(OutputType outputType)
        {
            this.outputType = outputType;
            return this;
        }

        public IPromptDesigns SetOutputUniqueness(OutputRandomness outputRandomness)
        {
            this.randomness = outputRandomness;
            return this;
        }

        public IPromptDesigns SetTask(PromptTask task)
        {
            this.promptTasks.TryAdd(task.Order, task);
            return this;
        }

        public IPromptDesigns SetTasks(params PromptTask[] tasks)
        {
            foreach (var task in tasks)
            {
                this.promptTasks[task.Order] = task;
            }
            return this;
        }
    }
}
