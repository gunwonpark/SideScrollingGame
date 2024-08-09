using System.Collections.Generic;

public abstract class DataBase<T>
{
    protected Dictionary<int, T> _datas = new Dictionary<int, T>();

    public void Init(string[] _datas)
    {
        LoadData(_datas);
    }
    public abstract void LoadData(string[] _datas);
    public virtual T GetData(int _id)
    {
        if (_datas.TryGetValue(_id, out T _value))
        {
            return _value;
        }

        return _value;
    }

}