namespace Farzin.Infrastructure.CrossCutting.Identity
{
    public class Role
    {
        private readonly string _name;
        private readonly int _id;

        public Role(int id, string name)
        {
            _id = id;
            _name = name;
        }
        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public override string ToString()
        {
            return _name;
        }
        public override bool Equals(object obj)
        {
            var r = obj as Role;
            return r != null && r._id == this._id;
        }
        public override int GetHashCode()
        {
            return this._name.GetHashCode();
        }
    }
}
