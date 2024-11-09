public interface IUsuario<T>
{
    public void Inset(T t){}

    public List<T> UsuarioList(){
        List<T> list = new List<T>();
        return list;
    }
}