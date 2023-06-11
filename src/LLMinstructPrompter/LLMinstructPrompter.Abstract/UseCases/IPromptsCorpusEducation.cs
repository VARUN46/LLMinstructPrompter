using LLMinstructPrompter.Abstract.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LLMinstructPrompter.Abstract.UseCases
{
    public interface IPromptsCorpusEducation
    {
        PromptableObject GetExamQuestionsPrompt(string description);

        PromptableObject GetExamEvaluationPrompt(string questions, string answers, string questionWithAnswers);

    }
}
