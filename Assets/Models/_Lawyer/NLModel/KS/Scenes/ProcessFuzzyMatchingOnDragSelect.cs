// using System.Collections;
// using System.Collections.Generic;

// using System.IO;

// using UnityEngine;

// using TMPro;
// using System;
// using UnityEngine.EventSystems;

// using FuzzySharp;
// using UnityEngine.UI;
// using System.Linq;
// using Sirenix.OdinInspector;
// using System.Text;
// using static UnityEngine.Rendering.DebugUI;

// namespace Naplap.NLUtils
// {
//     public class ProcessFuzzyMatchingOnDragSelect : MonoBehaviour
//     {
//         [BoxGroup("The Text Input Field")]
//         public TMP_InputField inputField;

//         [BoxGroup("The Text Input Field")]
//         public TMP_Text textSelected;
//         [BoxGroup("The Text Input Field")]
//         public string strSelectedText;

//         [BoxGroup("Debug Match Ratio")]
//         public int matchingRatio = 0;
//         [BoxGroup("Debug Match Ratio")]
//         public TMP_Text textMatchRatio;

//         [BoxGroup("Caret Info")]
//         public int caretPosition;
//         [BoxGroup("Caret Info")]
//         public int caretSelectPos;

//         [BoxGroup("File Settings")]
//         public string filePath = "Assets/Resources/Page1.txt";
//         [BoxGroup("File Settings")]
//         public string readString = string.Empty;

//         [BoxGroup("File Settings")]
//         public List<string> listString = new List<string>();

//         public List<TextMatchingRatio> listMMStruct = new List<TextMatchingRatio>();

//         public GameObject gameobjectListUI;
//         public GameObject prefabListItem;

//         bool bRequestRebuildListView = false;

//         // Start is called before the first frame update
//         void Start()
//         {
//             inputField.onTextSelection.AddListener(ProcessingOnTextSelection);
//             //inputField.onEndTextSelection.AddListener(ProcessingOnEndTextSelection);
            

//             StreamReader sr = new StreamReader(filePath);
//             readString = sr.ReadToEnd();
//             sr.Close();

//             string[] splitStr = readString.Split('\n');
//             listString.AddRange(splitStr);
//             foreach (string str in splitStr)
//             {
//                 TextMatchingRatio mms = new TextMatchingRatio();
//                 mms.textChunkToBeCompared = str;
//                 mms.matchedRatio = 0;
//                 listMMStruct.Add(mms);
//             }
//         }

//         // Update is called once per frame
//         void Update()
//         {
//             //textSelected.SetText(strSelectedText);
//             if (bRequestRebuildListView)
//             {
//                 bRequestRebuildListView = false;

//                 RebuildListView();
//             }

//             /*
//             if (bTextTagInserted) {
//                 bTextTagInserted = false;
//                 inputField.text = sb.ToString();
//                 inputField.Rebuild(CanvasUpdate.PreRender);
//             }
//             */
//         }


//         bool bTextTagInserted = false;
//         StringBuilder sb = new StringBuilder();
//         string highlightStartTag = "<color=#ff0000ff>";
//         string highlightEndTag = "</color>";
//         int startIndex, endIndex, selectionLength;

//         public void ProcessingOnEndTextSelection(string str, int a, int b) {
//             caretPosition = a;
//             caretSelectPos = b;
            

//             if (caretPosition < caretSelectPos)
//             {
//                 startIndex = caretPosition;
//                 endIndex = caretSelectPos;
//                 selectionLength = endIndex - startIndex;
//             }
//             else
//             {
//                 startIndex = caretSelectPos;
//                 endIndex = caretPosition;
//                 selectionLength = endIndex - startIndex;
//             }

//             // bold the selected text
//             sb.Clear();
//             sb.Append(inputField.text);

//             sb.Insert(startIndex, highlightStartTag);
//             sb.Insert(endIndex + highlightStartTag.Length, highlightEndTag);

//             bTextTagInserted = true;
//             //inputField.text = sb.ToString();
//             /////////////////////////
//         }

//         public void MouseEndDrag(BaseEventData bed) {
//             sb.Clear();
//             sb.Append(inputField.text);

//             sb.Insert(startIndex, highlightStartTag);
//             sb.Insert(endIndex + highlightStartTag.Length, highlightEndTag);

//             //bTextTagInserted = true;
//             inputField.text = sb.ToString();

//             inputField.ReleaseSelection();
//             //inputField.Rebuild(CanvasUpdate.PreRender);
//         }

//         public void ProcessingOnTextSelection(string str, int a, int b)
//         {
//             caretPosition = a;
//             caretSelectPos = b;            

//             if (caretPosition < caretSelectPos)
//             {
//                 startIndex = caretPosition;
//                 endIndex = caretSelectPos;
//                 selectionLength = endIndex - startIndex;
//             }
//             else
//             {
//                 startIndex = caretSelectPos;
//                 endIndex = caretPosition;
//                 selectionLength = endIndex - startIndex;
//             }

//             ///////
//             /*
//             sb.Clear();
//             sb.Append(inputField.text);

//             sb.Insert(startIndex, boldStartTag);
//             sb.Insert(endIndex + boldStartTag.Length, boldEndTag);

//             bTextTagInserted = true;
//             */
//             ////////////////////////////////////

//             //the selected text 
//             strSelectedText = inputField.text.Substring(startIndex, selectionLength);
//             //set and rebuild the TMP_Text 
//             textSelected.SetText(strSelectedText);
//             textSelected.Rebuild(CanvasUpdate.PreRender);

//             //Fuzzy matching the partial text
//             matchingRatio = Fuzz.Ratio(strSelectedText, inputField.text);
//             //set and rebuild the TMP_Text 
//             textMatchRatio.SetText(matchingRatio.ToString());
//             textMatchRatio.Rebuild(CanvasUpdate.PreRender);


//             //calculate all text matching
//             //remove all children
//             bRequestRebuildListView = true;
//         }

//         void RebuildListView()
//         {

//             foreach (Transform child in gameobjectListUI.transform)
//             {
//                 Destroy(child.gameObject);
//             }

//             for (int i = 0; i < listMMStruct.Count; i++)
//             {
//                 //listMMStruct[i].matchedRatio = Fuzz.Ratio(strSelectedText, listMMStruct[i].message);
//                 listMMStruct[i].matchedRatio = Fuzz.Ratio(strSelectedText, listMMStruct[i].textChunkToBeCompared);
//             }

//             InsertionSort(listMMStruct);

//             foreach (TextMatchingRatio mms in listMMStruct)
//             {
//                 GameObject g = Instantiate(prefabListItem, gameobjectListUI.transform);
//                 TMP_Text TMPMessage = g.transform.ChildContainsName("message").GetComponent<TMP_Text>();
//                 TMP_Text TMPRatio = g.transform.ChildContainsName("ratio").GetComponent<TMP_Text>();

//                 TMPMessage.SetText(mms.textChunkToBeCompared);
//                 TMPRatio.SetText(mms.matchedRatio.ToString());
//             }

//         }

//         public static void InsertionSort(List<TextMatchingRatio> input)
//         {
//             for (int i = 0; i < input.Count(); i++)
//             {
//                 var item = input[i];
//                 var currentIndex = i;

//                 while (currentIndex > 0 && input[currentIndex - 1].matchedRatio < item.matchedRatio)
//                 {
//                     input[currentIndex] = input[currentIndex - 1];
//                     currentIndex--;
//                 }

//                 input[currentIndex] = item;
//             }
//         }

//     }
// }