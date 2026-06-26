namespace PrintScreener;

internal static class Utility
{
    internal static string GetUniqueFullPath(string dirPath, string fileName, string fileType)
    {
        string fullPath = Path.Combine(dirPath, fileName + "." + fileType);
        if (!File.Exists(fullPath))
            return fullPath;

        int count = 2;
        while (File.Exists(fullPath))
        {
            fullPath = Path.Combine(dirPath, fileName + " (" + count + ")." + fileType);
            count++;
        }
        return fullPath;
    }

    internal static void FillComboBox(ComboBox comboBox, List<string> values, string? selectedValue = null)
    {
        int index = 0, selectedIndex = 0;
        comboBox.Items.Clear();
        foreach (string oneValue in values)
        {
            comboBox.Items.Add(oneValue);
            if (!string.IsNullOrWhiteSpace(selectedValue) && oneValue == selectedValue)
                selectedIndex = index;
            index++;
        }
        comboBox.SelectedIndex = selectedIndex;
    }
}
