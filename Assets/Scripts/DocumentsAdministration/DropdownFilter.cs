using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownFilter : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private TMP_Dropdown dropdown;

    private List<TMP_Dropdown.OptionData> dropdownOptions;

    public void OnEnable()
    {
        dropdownOptions = dropdown.options;
    }
    public void FilterDropdown()
    {
        dropdown.options = dropdownOptions.FindAll(option => option.text.IndexOf(inputField.text) >= 0);
    }
}
