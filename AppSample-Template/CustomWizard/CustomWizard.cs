using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;

namespace CustomWizard
{
    public class CustomWizard : IWizard
    {
        // This method is called before opening any item that 
        // has the OpenInEditor attribute.
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        // This method is only called for item templates,
        // not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem
            projectItem)
        {
        }

        // This method is called after the project is created.
        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
            var solutionName = replacementsDictionary["$safeprojectname$"];
            replacementsDictionary.Add("$myprojectnameparent$", solutionName);

            Guid g = Guid.NewGuid();
            replacementsDictionary.Add("$codeguidparent$", g.ToString());

            globalDictionary = new Dictionary<string, string>();
            globalDictionary.Add("$myprojectnameparent$", replacementsDictionary["$myprojectnameparent$"]);
            globalDictionary.Add("$codeguidparent$", replacementsDictionary["$codeguidparent$"]);

        }

        public static Dictionary<string, string> globalDictionary;

        // This method is only called for item templates,
        // not for project templates.
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
