/*
 * Autores
 * Santiago Morales Alvarez
 * Santiago Salgado
*/

using UnityEngine;
using UnityEditor;
using CharacterSystem;
using CharacterSystem.Skills;

namespace EditorView
{
    public class Character_ViewWindow : EditorWindow
    {


        private bool showingComposition = false, showingSkills = false;
        private bool builtAttributes = false;
        private Rect r = Rect.zero;
        private Vector2 scrollPos;
        private Character character;

        GUIStyle titleStyle = new GUIStyle(), subtitleStyle = new GUIStyle();

        [MenuItem("Window/NTC Workspace/Character")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(Character_ViewWindow));
        }




        private void ConfigStyles()
        {
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.fontSize = 16;

            subtitleStyle.fontStyle = FontStyle.Italic;
            subtitleStyle.fontSize = 12;
        }

        public void Update()
        {
            Repaint();
        }

        void OnGUI()
        {
            ConfigStyles();
            try
            {
                character = (Selection.activeObject as GameObject).GetComponent<Character>();
            }
            catch
            {

            }
            GUILayout.Space(10);

            if (character != null)
            {
                #region basic info
                GUILayout.Label(" Basic info", titleStyle);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Name: " + character.CharacterName);
                GUILayout.Label("State: " + character.State);
                GUILayout.EndHorizontal();

                GUILayout.Space(20);
                #endregion

                GUILayout.BeginArea(new Rect(8, 50, position.width - 8, position.height - 50));
                scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
                #region composition
                GUILayout.BeginHorizontal();
                GUILayout.Label("Composition", titleStyle);
                if (showingComposition)
                {
                    if (GUILayout.Button("Hide Composition"))
                    {
                        showingComposition = false;
                        builtAttributes = false;
                    }
                    /////////////////////////////////////////7
                    if (!builtAttributes)
                    {
                        if (GUILayout.Button("Build Composition"))
                        {
                            character.BuildAttributes();
                            builtAttributes = true;
                        }

                        GUILayout.EndHorizontal();
                        GUILayout.Space(10);
                    }
                    else
                    {
                        GUILayout.EndHorizontal();
                        GUILayout.Space(10);

                        #region life
                        GUILayout.Label("Life", subtitleStyle);
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Base: " + character.Attributes.LifeCharacter.BaseValue);
                        GUILayout.Label("Real Value: " + character.Attributes.LifeCharacter.Value);
                        GUILayout.Label("Regen Rate: " + character.Attributes.LifeCharacter.RegenerationRate);
                        GUILayout.Label("Current: " + character.Attributes.LifeCharacter.CurrentValue);
                        GUILayout.EndHorizontal();

                        r = EditorGUILayout.BeginVertical();
                        EditorGUI.ProgressBar(r, (character.Attributes.LifeCharacter.CurrentValue / character.Attributes.LifeCharacter.Value), "Current");
                        GUILayout.Space(30);
                        EditorGUILayout.EndVertical();
                        GUILayout.Space(15);
                        #endregion

                        #region energy
                        GUILayout.Label("Energy", subtitleStyle);
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Base: " + character.Attributes.EnergyCharacter.BaseValue);
                        GUILayout.Label("Real Value: " + character.Attributes.EnergyCharacter.Value);
                        GUILayout.Label("Regen Rate: " + character.Attributes.EnergyCharacter.RegenerationRate);
                        GUILayout.Label("Current: " + character.Attributes.EnergyCharacter.CurrentValue);
                        GUILayout.EndHorizontal();

                        r = EditorGUILayout.BeginVertical();
                        EditorGUI.ProgressBar(r, (character.Attributes.EnergyCharacter.CurrentValue / character.Attributes.EnergyCharacter.Value), "Current");
                        GUILayout.Space(30);
                        EditorGUILayout.EndVertical();
                        GUILayout.Space(15);
                        #endregion

                        #region defence
                        GUILayout.Label("Defence", subtitleStyle);
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Base: " + character.Attributes.DefenceCharacter.BaseValue);
                        GUILayout.Label("Real Value: " + character.Attributes.DefenceCharacter.Value);
                        GUILayout.EndHorizontal();
                        GUILayout.Space(15);
                        #endregion

                        #region speed
                        GUILayout.Label("Speed", subtitleStyle);
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Base: " + character.Attributes.SpeedCharacter.BaseValue);
                        GUILayout.Label("Real Value: " + character.Attributes.SpeedCharacter.Value);
                        GUILayout.EndHorizontal();
                        GUILayout.Space(15);
                        #endregion

                        #region tenacy
                        GUILayout.Label("Tenacy", subtitleStyle);
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Base: " + character.Attributes.TenacyCharacter.BaseValue);
                        GUILayout.Label("Real Value: " + character.Attributes.TenacyCharacter.Value);
                        GUILayout.EndHorizontal();
                        GUILayout.Space(15);
                        #endregion

                        #region Cognitive Control
                        GUILayout.Label("Cognitive Control", subtitleStyle);
                        GUILayout.BeginHorizontal();
                        GUILayout.EndHorizontal();
                        GUILayout.Space(15);
                        #endregion

                    }
                }
                else
                {
                    if (GUILayout.Button("Show Composition"))
                    {
                        showingComposition = true;
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.Space(20);
                #endregion

                #region Skills
                GUILayout.BeginHorizontal();
                GUILayout.Label("Skills info", titleStyle);
                if (showingSkills)
                {

                    if (GUILayout.Button("Hide Skills"))
                    {
                        showingSkills = false;
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(10);
                    /////////////////////////////////////////7

                    foreach (Skill t_skill in character.Skills)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(t_skill.SkillName, subtitleStyle);
                        GUILayout.Label("Level: " + t_skill.LevelSkill);
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Input | " + t_skill.Input);
                        GUILayout.Label("Input name | " + t_skill.InputName);
                        GUILayout.EndHorizontal();

                        GUILayout.Label("State : " + t_skill.State);

                        GUILayout.Label("Energy necessary : " + t_skill.Energy);

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Current cooldown : " + t_skill.CurrentCooldown);
                        GUILayout.Label("Cooldown : " + t_skill.Cooldown);
                        GUILayout.EndHorizontal();


                        Rect r = EditorGUILayout.BeginVertical();
                        EditorGUI.ProgressBar(r, (t_skill.CurrentCooldown / t_skill.Cooldown), "Current");
                        GUILayout.Space(30);
                        EditorGUILayout.EndVertical();

                        GUILayout.Space(15);
                    }
                }
                else
                {
                    if (GUILayout.Button("Show Skills"))
                    {
                        showingSkills = true;
                    }
                    GUILayout.EndHorizontal();
                }
                #endregion
                GUI.EndScrollView();
                GUILayout.EndArea();

            }
        }
    }
}