namespace ID.UI.Core.Roles
{
    public class RoleSearchFilter
    {
        private string? id;
        private string? name;

        public string? Id => id;
        public string? Name => name;

        public RoleSearchFilter WithId(string id)
        {
            this.id = id;

            return this;
        }

        public RoleSearchFilter WithName(string name)
        {
            this.name = name;

            return this;
        }
    }
}
