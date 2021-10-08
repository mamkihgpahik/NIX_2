using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NIX_2.Entities;
using NIX_2.Repositories;
using System.IO;

namespace NIX_2.Repositories
{
    abstract class Repository<T>
    {
        protected readonly string _address;
        protected char sep = '|';
        public Repository(string address)
        {
            _address = address;
        }

        public abstract void Add(T elem);
        public virtual bool Delete(int id)
        {
            bool isDeleted = false;
            string tempFile = Path.GetTempFileName();
            using (var f = File.Open(_address, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var f2 = File.Open(tempFile, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    using (var sr = new StreamReader(f))
                    {
                        using (var sw = new StreamWriter(f2))
                        {
                            while (sr.Peek() > -1)
                            {
                                string[] line = sr.ReadLine().Split(sep);
                                if (Convert.ToInt32(line[0]) != id)
                                {                                   
                                    for (int i = 0; i < line.Length; i++)
                                    {
                                        sw.Write($"{line[i]}");
                                        if (i<line.Length-1)
                                        {
                                            sw.Write($"{sep}");
                                        }
                                        else sw.WriteLine();
                                    }                               
                                }
                                else isDeleted = true;
                            }
                        }
                    }
                }
            }
            File.Delete(_address);
            File.Move(tempFile, _address);
            return isDeleted;
        }
   
        public virtual List<T> GetAll()
        {
            List<T> _ts = new List<T>();
            using (var f = File.Open(_address, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (var sr = new StreamReader(f))
                {
                    string[] _buff;
                    while (sr.Peek() > -1)
                    {
                        _buff = sr.ReadLine().Split(sep);
                        T obj = (T)Activator.CreateInstance(typeof(T), _buff);
                        _ts.Add(obj);     
                    }
                }
           
            }
            return _ts;
        }
        public virtual T Search(int id)
        {
            string[] _buff;
            using (var f = File.Open(_address, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(f))
                {
                    while (sr.Peek() > -1)
                    {
                        _buff = sr.ReadLine().Split(sep);
                        if (Int32.Parse(_buff[0]) == id)
                        {                            
                            return (T)Activator.CreateInstance(typeof(T), args: _buff);
                        }
                    }
                }
            }
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
