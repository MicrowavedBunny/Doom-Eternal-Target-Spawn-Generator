using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpawnTargetGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            string start = @"
                entity {
                    entityDef ";

            string template = @" {
                    inherit = ""target/spawn"";
                    class = ""idTarget_Spawn"";
                    expandInheritance = false;
                    poolCount = 0;
                    poolGranularity = 2;
                    networkReplicated = false;
                    disableAIPooling = false;
                    edit = {
                        flags = {
                            noFlood = true;
                        }
                        spawnConditions = {
                                maxCount = 0;
                                reuseDelaySec = 0;
                                doBoundsTest = false;
                                boundsTestType = ""BOUNDSTEST_NONE"";
                                fovCheck = 0;
                                minDistance = 0;
                                maxDistance = 0;
                                neighborSpawnerDistance = -1;
                                LOS_Test = ""LOS_NONE"";
                                playerToTest = ""PLAYER_SP"";
                                conditionProxy = """";
                            }
                        spawnEditableShared = {
                            groupName = """";
                            deathTrigger = """";
                            coverRadius = 0;
                            maxEnemyCoverDistance = 0;
                        }
                        conductorEntityAIType = ""SPAWN_AI_TYPE_ANY"";
                        initialEntityDefs = {
                            num = 0;
                        }
                        spawnEditable = {
                            spawnAt = """";
                            copyTargets = false;
                            additionalTargets = {
                                num = 0;
                            }
                            overwriteTraversalFlags = true;
                            traversalClassFlags = ""CLASS_A"";
                            combatHintClass = ""CLASS_ALL"";
                            spawnAnim = """";
                            aiStateOverride = ""AIOVERRIDE_TELEPORT"";
                            initialTargetOverride = """";
                        }
                        portal = """";
                        targetSpawnParent = """";
                        disablePooling = false;";

            string ending = @"
                    }
                }
                }";

            string startList = "\n            item[";
            string midList = @"] = """;
            string endList = @""";";

            if (args.Length == 0)
                return; // return if no file was dragged onto exe

            
            //fix tabbing issue with this??
            string text = File.ReadAllText(args[0]);
            string[] splitText = text.Split(',');


            Console.WriteLine("Enter naming scheme: Ex. e12_spawn_");
            string namingScheme = Console.ReadLine();
            Console.WriteLine("Enter starting number spawn names:");
            int spawnStartingNum = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter starting number for spawn group appending:");
            int startingNum = int.Parse(Console.ReadLine()); 



            int[] itemNumber = Enumerable.Range(startingNum, splitText.Length).ToArray();
            int[] spawnStartNumberArray = Enumerable.Range(spawnStartingNum, splitText.Length).ToArray();

            string[] generatedspawns = new string[splitText.Length];
            string[] items = new string[splitText.Length];

            for (int i = 0; i < splitText.Length; i++)
            {
                generatedspawns[i] = (start + namingScheme + spawnStartNumberArray[i] + template + "\n" + splitText[i] + ending + "\n");
                items[i] = (startList + itemNumber[i] + midList + namingScheme + (i + 1) + endList);
            }

            //copy above and use it to make the spawn group items using namingscheme and i for names and i for item numbers (allow user to input starting number
            //Enumerable.Range(30, 21).ToArray();



            string SpawnResult = String.Concat(generatedspawns);
            string listResult = String.Concat(items);
            string fullResult = SpawnResult + "\n" + listResult; 


            string path = Path.GetDirectoryName(args[0])
            + Path.DirectorySeparatorChar
            + "generatedSpawns" + Path.GetExtension(args[0]);
            File.WriteAllText(path, fullResult);
        }
    }
}
