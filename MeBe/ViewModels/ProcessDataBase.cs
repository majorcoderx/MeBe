using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SQLite;
using System.Collections.ObjectModel;
using MeBe.Models;

namespace MeBe.ViewModels
{
    public class ProcessDataBase
    {
        public static string dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Data.sqlite");

        public virtual ObservableCollection<object> GetListObject()
        {
            using (var db = new SQLiteConnection(ProcessDataBase.dbPath))
            {
                List<object> obCollection = db.Table<object>().ToList<object>();
                ObservableCollection<object> listNote = new ObservableCollection<object>(obCollection);
                return listNote;
            }
        }
        public virtual object GetObject(object T) { return T; }
        public virtual void InsertObject(object T)
        {
            using (var db = new SQLiteConnection(ProcessDataBase.dbPath))
            {
                db.RunInTransaction(() =>
                {
                    db.Insert(T);
                });
            }
        }
        public virtual void DeleteObject(object T) { }
        public virtual void UpdateObject(object T) { }
    }

    public class PNote : ProcessDataBase
    {
        public new ObservableCollection<Notes> GetListObject()
        {
            using (var db = new SQLiteConnection(ProcessDataBase.dbPath))
            {
                Notes temp = new Notes();
                List<Notes> obCollection = db.Table<Notes>().ToList<Notes>();
                for (int i = 0,j=obCollection.Count-1; i < obCollection.Count; ++i)
                {
                    if (i < obCollection.Count / 2)
                    {
                        temp = obCollection[i];
                        obCollection[i] = obCollection[j];
                        obCollection[j] = temp;
                        --j;
                    }
                    else break;
                }
                ObservableCollection<Notes> listNote = new ObservableCollection<Notes>(obCollection);
                return listNote;
            }
        }
        public void UpdateObject(Notes note)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
               // var existing = db.Query<Notes>("select * from Notes where id = " + note.ID).FirstOrDefault();
                db.RunInTransaction(() => {
                    db.Update(note);
                });
            }
        }
        public void DeleteObject(Notes note)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                var existing = db.Query<Notes>("select * from Notes where id = " + note.ID).FirstOrDefault();
                db.RunInTransaction(() =>
                {
                    db.Delete(note);
                });
            }
        }
    }

    public class PBabyCheckHeight : ProcessDataBase
    {
        public HeightFor GetObject(int month,string table)
        {
            System.Diagnostics.Debug.WriteLine("month : " + month + " " + table);
            using (var db = new SQLiteConnection(dbPath))
            {
                var existing = db.Query<HeightFor>("select * from " + table + " where Month = "+ month).FirstOrDefault();
                return existing;
            }
        }
    }

    public class PBabyCheckWeight : ProcessDataBase
    {
        public WeightFor GetObject(int month,string table)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                var existing = db.Query<WeightFor>("select * from " + table + " where Month = " + month).FirstOrDefault();
                return existing;
            }
        }
    }

    public class PBabyCheck : ProcessDataBase
    {
        //insert lan check - ke thua method nay
        public new ObservableCollection<BabyCheck> GetListObject()
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                BabyCheck temp = new BabyCheck();
                List<BabyCheck> obCollection = db.Table<BabyCheck>().ToList<BabyCheck>();

                for(int i = 0,j = obCollection.Count-1;i<obCollection.Count;++i)
                {
                    if(i<obCollection.Count/2)
                    {
                        temp = obCollection[i];
                        obCollection[i] = obCollection[j];
                        obCollection[j] = temp;
                    }
                }
                ObservableCollection<BabyCheck> listBabyCheck = new ObservableCollection<BabyCheck>(obCollection);
                return listBabyCheck;
            }
        }

        public void UpdateObject(BabyCheck baby)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
               // var existing = db.Query<BabyCheck>("").FirstOrDefault();
                db.RunInTransaction(() =>
                {
                    db.Update(baby);
                });
            }
        }
    }

    public class PMamaCheck : ProcessDataBase
    {
        //insert lan check- ke thua method nay
        public MamaCheck GetObject(int week)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                var existing = db.Query<MamaCheck>("select * from MamaCheck where Week = "+(week-1)).FirstOrDefault();
                return existing;
            }
        }

        public new ObservableCollection<MamaCheck> GetListObject()
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                MamaCheck temp = new MamaCheck();
                List<MamaCheck> obCollection = db.Table<MamaCheck>().ToList<MamaCheck>();
                for (int i = 0, j = obCollection.Count - 1; i < obCollection.Count; ++i)
                {
                    if (i < obCollection.Count / 2)
                    {
                        temp = obCollection[i];
                        obCollection[i] = obCollection[j];
                        obCollection[j] = temp;
                    }
                }
                ObservableCollection<MamaCheck> mamaList = new ObservableCollection<MamaCheck>(obCollection);
                return mamaList;
            }
        }

        public void UpdateObject(MamaCheck mama)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                //var existing = db.Query<MamaCheck>("select * from MamaCheck where Week = "+mama.Week).FirstOrDefault();
                db.RunInTransaction(() =>
                {
                    db.Update(mama);
                });
            }
        }
    }

    public class PMamaCheckWeight : ProcessDataBase
    {
        public WeightForMama GetObject(int week)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                var existing = db.Query<WeightForMama>("select * from WeightForMama where Week = "+week).FirstOrDefault();
                return existing;
            }
        }
    }

    public class PMamaBMI : ProcessDataBase
    {
        public new ObservableCollection<WeightMamaBMI> GetListObject()
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                List<WeightMamaBMI> obCollection = db.Table<WeightMamaBMI>().ToList<WeightMamaBMI>();
                WeightMamaBMI temp = new WeightMamaBMI();
                for (int i = 0, j = obCollection.Count - 1; i < obCollection.Count; ++i)
                {
                    if (i < obCollection.Count / 2)
                    {
                        temp = obCollection[i];
                        obCollection[i] = obCollection[j];
                        obCollection[j] = temp;
                    }
                }
                ObservableCollection<WeightMamaBMI> listWeight = new ObservableCollection<WeightMamaBMI>(obCollection);
                return listWeight;
            }
        }
    }

    public class PKnowledge : ProcessDataBase
    {
        public ObservableCollection<Knowledges> GetListObject(int week,int numb,int state)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                int id;
                List<Knowledges> obCollection = new List<Knowledges>();
                var existing = db.Query<Knowledges>("select * from knowledges where week = " + week + " and state = " + state).FirstOrDefault();
                
                while (existing != null)
                {
                    id = existing.ID;
                    existing.ExContent = existing.Content.Substring(0, 90) + "...";
                    obCollection.Add(existing);
                    existing = db.Query<Knowledges>("select * from knowledges where week = " + week + " and state = " + state + " and id = " + (id + 1)).FirstOrDefault();
                    if (existing == null && numb > 0)
                    {
                        week += 1;
                        numb -= 1;
                        existing = db.Query<Knowledges>("select * from knowledges where week = " + week + " and state = " + state + " and id = " + (id + 1)).FirstOrDefault();
                    }
                }   
                ObservableCollection<Knowledges> listKnowledge = new ObservableCollection<Knowledges>(obCollection);
                return listKnowledge;
            }
        }

        public ObservableCollection<Knowledges> GetListObject(int state)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                List<Knowledges> obCollection = db.Query<Knowledges>("select * from knowledges where like = 1 and state = "+state).ToList<Knowledges>();
                ObservableCollection<Knowledges> listLike = new ObservableCollection<Knowledges>(obCollection);
                return listLike;
            }
        }

        public void UpdateObject(Knowledges know)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.RunInTransaction(() =>
                {
                    db.Update(know);
                });
            }
        }
    }

    public class PNutrition : ProcessDataBase
    {
        public new ObservableCollection<Food> GetListObject()
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                List<Food> obCollection = db.Table<Food>().ToList<Food>();
                ObservableCollection<Food> listFood = new ObservableCollection<Food>(obCollection);
                return listFood;
            }
        }
    }

    public class PTestForMama : ProcessDataBase
    {
        public ObservableCollection<TestMama> GetListObject(int type, int month)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                List<TestMama> obCollection = db.Query<TestMama>("select * from TestMama where type = "+type+" and month = "+month).ToList<TestMama>();
                ObservableCollection<TestMama> listTest = new ObservableCollection<TestMama>(obCollection);
                return listTest;
            }
        }

        public void UpdateObject(TestMama test)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
               //var existing = db.Query<TestMama>("select * from TestMMama where ID = " + test.ID).FirstOrDefault();
                db.RunInTransaction(() =>
                {
                    db.Update(test);
                });
            }
        }
    }

    public class PAddressHospital : ProcessDataBase
    {
        public ObservableCollection<HospitalAddress> GetListObject(int type)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                List<HospitalAddress> obCollection = 
                    db.Query<HospitalAddress>("select * from HospitalAddress where type = " + type).ToList<HospitalAddress>();
                ObservableCollection<HospitalAddress> listHospitalAdd = new ObservableCollection<HospitalAddress>(obCollection);
                return listHospitalAdd;
            }
        }
    }

    public class PNotification : ProcessDataBase
    {

    }
}
