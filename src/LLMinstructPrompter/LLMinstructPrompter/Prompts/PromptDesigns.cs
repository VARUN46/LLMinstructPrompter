using LLMinstructPrompter.Abstract.Entities;
using LLMinstructPrompter.Abstract.Prompts;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LLMinstructPrompter.Prompts
{
    public class PromptDesigns : IPromptDesigns
    {
        private OutputType outputType;
        private List<string> allKeys;
        private OutputRandomness randomness;
        private bool isList;
        private Dictionary<int, PromptTask> promptTasks = new Dictionary<int, PromptTask>();
        private bool discardSysPrompt;
        private string sysPrompt;

        public PromptableObject GetPrompt()
        {
            var promptTemplate = JsonConvert.DeserializeObject<PromptTemplates>(File.ReadAllText("Templates/promptTemplates.json"));

            var givenFormat = GetOutputPromptPart(outputType);
            string mainPrompt = string.Empty;
            if (!discardSysPrompt)
            {
                if (string.IsNullOrWhiteSpace(sysPrompt))
                {
                    mainPrompt = promptTemplate.SystemPromptTemplate;
                }
                else
                {
                    mainPrompt = sysPrompt;
                }
            }
            if (promptTasks.Count == 1)
            {
                mainPrompt = promptTemplate.PromptBuilder1TaskTemplateGeneric.Replace("{task}", promptTasks.First().Value.Task).Replace("{given_format}", givenFormat);
            }
            else
            {
                var taskSteps = new StringBuilder();
                foreach (var task in promptTasks.OrderBy(c => c.Key))
                {
                    taskSteps.AppendLine(promptTemplate.PromptBuilderTaskTemplate.Replace("{step_no}", task.Key.ToString()).Replace("{given_task}", task.Value.Task));
                }
                taskSteps.AppendLine("Note: Only generate the output for final task. Do not generate any additional information.");
                mainPrompt = promptTemplate.PromptBuilderNTaskTemplateGeneric.Replace("{tasks}", taskSteps.ToString()).Replace("{given_format}", givenFormat);
            }
            var promptableObject = new PromptableObject
            {
                UserPrompt = mainPrompt,
                OutputRandomnessType = randomness,
            };
            return promptableObject;
        }

        private string GetOutputPromptPart(OutputType outputType)
        {
            var givenFormat = string.Empty;
            if (outputType == OutputType.CSV)
            {
                givenFormat = $"Generate the output in format:\nType should be {outputType}\nKeys: {string.Join(",", allKeys)}";
            }
            else if (outputType == OutputType.JSON)
            {
                string listPart = isList ? " list" : "";
                string objectFormat = "{ " + string.Join(",", allKeys.Select(c => $"\"{c}\":\"<{c.ToLower()}>\"")) + " }";
                givenFormat = $"Generate the output in format:\nType should be {outputType} {listPart}, object as {objectFormat}";

            }
            else if (outputType == OutputType.BulletPoints)
            {
                givenFormat = "Generate the output in bullet points.";
            }
            return givenFormat;
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

        public IPromptDesigns OutputObject<T>()
        {
            isList = typeof(T).GetInterfaces().Any(c => c.Name == "IEnumerable");
            if (isList)
            {
                var keys = typeof(T).GetProperties().First(c => c.Name == "Item").PropertyType.GetProperties();
                allKeys = keys.Select(c => c.Name).ToList();

            }
            else
            {
                var keys = typeof(T).GetProperties();
                allKeys = keys.Select(c => c.Name).ToList();
            }
            return this;
        }

        public IPromptDesigns SetSystemPrompt(string prompt)
        {
            this.sysPrompt = prompt;
            return this;
        }

        public IPromptDesigns DiscardSystemPrompt()
        {
            this.discardSysPrompt = true;
            return this;
        }
    }
}
