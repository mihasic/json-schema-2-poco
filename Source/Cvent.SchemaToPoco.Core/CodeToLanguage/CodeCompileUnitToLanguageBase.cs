using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;

namespace Cvent.SchemaToPoco.Core.CodeToLanguage
{
    /// <summary>
    /// Controller for converting a CodeCompileUnit to code given a code provider.
    /// </summary>
	public class CodeCompileUnitToLanguageBase
	{
        /// <summary>
        /// The abstract representation of the code.
        /// </summary>
		private readonly CodeCompileUnit _codeCompileUnit;

        /// <summary>
        /// Constant represented a tab.
        /// </summary>
		private const string TabCharacter = "\t";

		public CodeCompileUnitToLanguageBase(CodeCompileUnit codeCompileUnit)
		{
			if (codeCompileUnit == null) throw new ArgumentNullException("codeCompileUnit");
			_codeCompileUnit = codeCompileUnit;
		}

        /// <summary>
        /// Convert the abstract representation of the code to functional code given a code provider.
        /// </summary>
        /// <param name="codeProvider">The code provider.</param>
        /// <returns>The code as a string.</returns>
		protected string CodeUnitToLanguage(CodeDomProvider codeProvider)
		{
			var stringBuilder = new StringBuilder();
			var stringWriter = new StringWriter(stringBuilder);
			var writer = new IndentedTextWriter(stringWriter, TabCharacter);
			codeProvider.GenerateCodeFromCompileUnit(_codeCompileUnit, writer, new CodeGeneratorOptions
			{
				BlankLinesBetweenMembers = true,
				VerbatimOrder = false,
				BracingStyle = "C"
			});

			var output = stringBuilder.ToString();
			return output;
		}
	}
}