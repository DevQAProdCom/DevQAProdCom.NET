namespace DevQAProdCom.NET.API.Models
{
    public class Header
    {
        public string Name { get; set; }
        public List<string?> Values { get; set; }

        public Header(string name, params string[] values)
        {
            Name = name;
            Values = values?.ToList();
        }

        public void AddValues(params string[] values)
        {
            Values ??= new List<string?>();

            if (values?.Length > 0)
                Values.AddRange(values);
        }

        public void ClearAndAddValues(params string[] values)
        {
            Values?.Clear();
            AddValues(values);
        }
    }
}
