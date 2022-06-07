using UnityEngine;
using UnityEngine.UI;

namespace Menus_UI
{
    public class CharacterSelect : MonoBehaviour
    {
        public string[] characterNames;
        public Sprite[] characterImage;

        public Text selectedName;
        public Image selectedImage;

        public void Start()
        {
            GameState.Instance.selectedCharacter = -1;
        }

        public void Cop()
        {
            selectedName.text = characterNames[0];
            selectedImage.sprite = characterImage[0];
            GameState.Instance.selectedCharacter = 0;
        }
    
        public void Cricket()
        {
            selectedName.text = characterNames[1];
            selectedImage.sprite = characterImage[1];
            GameState.Instance.selectedCharacter = 1;
        }
    
        public void Witch()
        {
            selectedName.text = characterNames[2];
            selectedImage.sprite = characterImage[2];
            GameState.Instance.selectedCharacter = 2;
        }
    
        public void Devil()
        {
            selectedName.text = characterNames[3];
            selectedImage.sprite = characterImage[3];
            GameState.Instance.selectedCharacter = 3;
        }
    
        public void Doctor()
        {
            selectedName.text = characterNames[4];
            selectedImage.sprite = characterImage[4];
            GameState.Instance.selectedCharacter = 4;
        }
        
        public void Farmer()
        {
            selectedName.text = characterNames[5];
            selectedImage.sprite = characterImage[5];
            GameState.Instance.selectedCharacter = 5;
        }
        
        public void Ghost()
        {
            selectedName.text = characterNames[6];
            selectedImage.sprite = characterImage[6];
            GameState.Instance.selectedCharacter = 6;
        }
    
        public void Knight()
        {
            selectedName.text = characterNames[7];
            selectedImage.sprite = characterImage[7];
            GameState.Instance.selectedCharacter = 7;
        }
    
        public void Nurse()
        {
            selectedName.text = characterNames[8];
            selectedImage.sprite = characterImage[8];
            GameState.Instance.selectedCharacter = 8;
        }
    
        public void Patient()
        {
            selectedName.text = characterNames[9];
            selectedImage.sprite = characterImage[9];
            GameState.Instance.selectedCharacter = 9;
        }
    
        public void Pilgrim()
        {
            selectedName.text = characterNames[10];
            selectedImage.sprite = characterImage[10];
            GameState.Instance.selectedCharacter = 10;
        }
    
        public void Robber()
        {
            selectedName.text = characterNames[11];
            selectedImage.sprite = characterImage[11];
            GameState.Instance.selectedCharacter = 11;
        }
    
        public void Santa()
        {
            selectedName.text = characterNames[12];
            selectedImage.sprite = characterImage[12];
            GameState.Instance.selectedCharacter = 12;
        }
    
        public void Streaker()
        {
            selectedName.text = characterNames[13];
            selectedImage.sprite = characterImage[13];
            GameState.Instance.selectedCharacter = 13;
        }
    
        public void Zombie()
        {
            selectedName.text = characterNames[14];
            selectedImage.sprite = characterImage[14];
            GameState.Instance.selectedCharacter = 14;
        }
    }
}
