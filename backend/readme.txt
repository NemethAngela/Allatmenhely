Model-ek és a DB context legenerálása adatbázisból (DB first megközelítés):

dotnet ef dbcontext scaffold "server=allatmenhely-allatmenhely.a.aivencloud.com;port=26431;database=defaultdb;user id=avnadmin;password=AVNS_3mZIl5CUrteFcbMPwf_" Pomelo.EntityFrameworkCore.MySql -o Models
