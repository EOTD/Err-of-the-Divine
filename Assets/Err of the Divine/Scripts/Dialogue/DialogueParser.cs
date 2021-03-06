﻿using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

public class DialogueParser : MonoBehaviour {

    List<DialogueLine> lines;
    public List<Sprite> images;

    struct DialogueLine {
        public string name;
        public string content;
        public int pose;
        public string position;

        public DialogueLine(string n, string c, int p, string pos) {
            name = n;
            content = c;
            pose = p;
            position = pos;
        }
    }


	// Use this for initialization
	void Start () {
        string file = "Assets/Err of the Divine/Resources/Dialogues/Dialogue";
        //string sceneNum = EditorApplication.currentScene; //"Scene"
        //sceneNum = Regex.Replace(sceneNum, "[^0-9]", "");
        //file += sceneNum;
        file += ".txt";

        lines = new List<DialogueLine>();
        LoadDialogue(file);

        images = new List<Sprite>();
        LoadImages();
	}

    public string GetName(int lineNumber) {
        if(lineNumber < lines.Count) {
            return lines[lineNumber].name;
        }
        return "";
    }

    public string GetContent(int lineNumber) {
        if (lineNumber < lines.Count) {
            return lines[lineNumber].content;
        }
        return "";
    }

    public Sprite GetPose(int lineNumber) {
        if (lineNumber < lines.Count) {
            return images[lines[lineNumber].pose];
        }
        return null;
    }

    public string GetPosition(int lineNumber) {
        if (lineNumber < lines.Count) {
            return lines[lineNumber].position;
        }
        return "";
    }

    void LoadImages() {
        for (int i = 0; i < lines.Count; i++) {
            string imageName = lines[i].name;
            Sprite image = (Sprite)Resources.Load("Dialogues/Poses/"+imageName, typeof(Sprite));
            if (!images.Contains(image)) {
                images.Add(image);
            }
        }
    }

    void LoadDialogue(string filename) {
        string file = "Assets/Err of the Divine/Resources/Dialogues/" + filename;
        string line;
        StreamReader r = new StreamReader(filename);

        using (r) {
            do {
                line = r.ReadLine();
                if (line != null) {
                    string[] line_values = SplitCsvLine(line);
                    DialogueLine line_entry = new DialogueLine(line_values[0],line_values[1],int.Parse(line_values[2]),line_values[3]);
                    lines.Add(line_entry);

                }
            }
            while (line != null);
            r.Close();
        }
    }

    // splits a CSV row 
    private string[] SplitCsvLine(string line) {
        string pattern = @"
     # Match one value in valid CSV string.
     (?!\s*$)                                      # Don't match empty last value.
     \s*                                           # Strip whitespace before value.
     (?:                                           # Group for value alternatives.
       '(?<val>[^'\\]*(?:\\[\S\s][^'\\]*)*)'       # Either $1: Single quoted string,
     | ""(?<val>[^""\\]*(?:\\[\S\s][^""\\]*)*)""   # or $2: Double quoted string,
     | (?<val>[^,'""\s\\]*(?:\s+[^,'""\s\\]+)*)    # or $3: Non-comma, non-quote stuff.
     )                                             # End group of value alternatives.
     \s*                                           # Strip whitespace after value.
     (?:,|$)                                       # Field ends on comma or EOS.
     ";
        string[] values = (from Match m in Regex.Matches(line, pattern,
            RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline)
                           select m.Groups[1].Value).ToArray();
        return values;
    }
}
