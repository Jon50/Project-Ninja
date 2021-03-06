using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace SOM{
	/// <summary>
	/// This class is responsible for converting the .xml file into a C# file
	/// </summary>
	public static class SOMCSHarpHandler{

		//========================================
		//Consts
		//========================================
		const string NAME = "StringOMatic";
		const string WITH_ERRORS = "WithErrors";

		//========================================
		//Properties
		//========================================
		static string _path;
		static string path{
			get{
				if (_path == null){
					string[] folders = Directory.GetDirectories("Assets/","StringOMatic*",SearchOption.AllDirectories);
					if (folders.Length == 0)
						throw new FileNotFoundException("StringOMatic folder could not be found");
					if (folders.Length > 1)
						throw new FileLoadException("More than one StringOMatic folder were found");
					_path = folders[0]+"/";
				}
				return _path;
			}
		}

		//========================================
		//Methods
		//========================================
		/// <summary>
		/// Generates a new C# file from the xml file. If the new file contains any compilation errors, it is created as a .txt file instead.
		/// </summary>
		public static void Compile(){
			if (!SOMXmlHandler.DocumentExists())
				throw new XmlDocumentDoesNotExistException();

			StringBuilder file = new StringBuilder();

			//START WRITING
			file.AppendLine("/*=========================================================================");
			file.AppendLine("THIS DOCUMENT IS AUTOGENERATED AND SHOULD NOT BE MANUALLY EDITED OR DELETED");
			file.AppendLine("===========================================================================*/");
			file.AppendLine();
			file.AppendLine("public static class " + NAME);
			file.AppendLine("{");

			string[] modules = SOMXmlHandler.GetAllModules();
			int lastIndentation = 0;

			for (int i = 0; i < modules.Length; i++){
				//Deal with opening indentation
				int indentation = modules[i].Split('.').Length; 
				if (i!= 0){
					for (int j = 0; j <= lastIndentation-indentation; j++){
						WriteIndentation(file, lastIndentation-j);
						file.AppendLine("}");
					}
				}

				//Each module is represented as a public static class
				WriteIndentation(file, indentation);
				file.AppendLine("public static class " + SOMUtils.NicifyModuleName(modules[i].Substring(modules[i].LastIndexOf(".")+1)));
				WriteIndentation(file, indentation);
				file.AppendLine("{");

				//Each constant, as a public string constant
				KeyValuePair<string, string>[] constants = SOMXmlHandler.GetAllConstants(modules[i]);
				for (int j = 0; j < constants.Length; j++){
					WriteIndentation(file, indentation+1);
					file.AppendLine("public const string "+constants[j].Key+" = \"" + constants[j].Value+"\";");
				}

				lastIndentation = indentation;
			}
			//Deal with closing indentation
			for (int i = 0; i <= lastIndentation; i++){
				WriteIndentation(file, lastIndentation-i);
				file.AppendLine("}");
			}
			//Check if the code can be compiled
			bool canBeCompiled = true;
			CSharpCodeProvider provider = new CSharpCodeProvider();
			CompilerResults results = provider.CompileAssemblyFromSource(new CompilerParameters(), file.ToString());
			for (int i = 0; i < results.Errors.Count; i++){
				if (!results.Errors[i].IsWarning){
					canBeCompiled = false;
					break;
				}
			}

			string output = path+NAME;
			//If the code can't be compiled, create a new text file with the suffix WITH_ERRORS
			if (!canBeCompiled){
				string log = "The newly created "+NAME+".cs file contains the compilation errors listed below. " +
					"In order to maintain previous code stability and avoid trouble, the file has been renamed to "+NAME+WITH_ERRORS+".txt. That'll teach it!\n\n";
				for (int i = 0; i < results.Errors.Count; i++)
					if (!results.Errors[i].IsWarning)
						log+= "Line " + (results.Errors[i].Line+1)+" --> error " + results.Errors[i].ErrorNumber+": "+results.Errors[i].ErrorText+"\n";
				SOMUtils.LogError(log);
				output+=WITH_ERRORS;
				output+=".txt";
			}
			else{
				output+=".cs";
				SOMUtils.Log("Compilation Successfull");
			}
			File.WriteAllText(output, file.ToString());
			AssetDatabase.ImportAsset(output.Substring(output.IndexOf("Assets/")));
		}
		static void WriteIndentation(StringBuilder file, int indentation){
			StringBuilder value = new StringBuilder();
			for (int i = 0; i < indentation; i++)
				value.Append("\t");
			file.Append(value.ToString());
		}
	}
}