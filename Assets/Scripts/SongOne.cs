using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongOne : MonoBehaviour {

    public static int totalNotes = 0;

    // Row, Octave, Key, Time (1 = 1/4), Measure, Where in Measure (1/4), Background Note (0 or 1)
    public static List<List<List<float>>> keyArray;

    public static int tempo = 80;

    void Start() {
        keyArray = new List<List<List<float>>>();
        TextAsset songData = Resources.Load<TextAsset>("songone");
        string[] data = songData.text.Split(new char[] {'\n'});
        List<List<float>> chord = new List<List<float>>();
        for (int i = 1; i < data.Length; i++) {
            string[] stringRow = data[i].Split(new char[] {','});
            bool isNewNote = stringRow[0] == "TRUE" ? true : false;
            if (isNewNote) {
                if (chord.Count != 0) {
                    totalNotes++;
                    keyArray.Add(chord);
                }
                chord = new List<List<float>>();
            }
            float row = float.Parse(stringRow[1]);
            float octave = float.Parse(stringRow[2]);
            float key = float.Parse(stringRow[3]);
            float time = float.Parse(stringRow[4]);
            float measure = float.Parse(stringRow[5]);
            float where = float.Parse(stringRow[6]);
            float background = stringRow[7][0] == 'F' ? 0 : 1;
            List<float> keyNote = new List<float>() {
                row, octave, key, time, measure, where, background
            };
            chord.Add(keyNote);
        }
        totalNotes++;
        keyArray.Add(chord);
    }
}
