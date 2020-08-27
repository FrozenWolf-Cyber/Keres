import mysql.connector
import random
import string
import math

class match_control:
    def __init__(self,host,user,passwd,database):
        self.db = mysql.connector.connect(host=host,
                                     user=user,
                                     passwd=passwd,
                                     database=database)


        self.cursor = self.db.cursor()


        self.matches_db = mysql.connector.connect(host=host,
                                     user=user,
                                     passwd=passwd,
                                     database=database)

        self.match_cursor = self.matches_db.cursor()

    def generate_unique_id(self):
        unique_id = ''
        for i in range(random.randrange(8,15)):
            unique_id = unique_id + random.choice(string.printable[:61])

        return unique_id


    def add_player_to_que(self,player_id,player_stats):

        for i in range(len(player_stats)):
            player_stats[i] = int(player_stats[i])
        level , games_won , games_lost , games_drawn  = tuple(player_stats)
        player_powerlevel = 0

        if games_won + games_drawn + games_lost == 0:
            player_powerlevel = 0

        else:
            player_powerlevel = level*10 + (games_won/games_lost)*15 + games_drawn

        self.cursor.execute('UPDATE PLAYER_INFO SET state = "CANCEL" WHERE id = %s',(player_id,))

        self.cursor.execute('INSERT INTO MATCH_MAKING (id , level , player_power , room_id) VALUES (%s,%s,%s,%s)',((str(player_id)),str(level),str(player_powerlevel),"none"))
        self.db.commit()
        self.cursor.close()
        self.db.close()


    def remove_player(self,player_id):
        self.cursor.execute("DELETE FROM MATCH_MAKING WHERE id = %s",(player_id,))
        self.cursor.execute('UPDATE PLAYER_INFO SET state = "START" WHERE id = %s', (player_id,))
        self.db.commit()

    def check_for_players(self,player_id):
        self.cursor.execute('SELECT player_power,level,room_id FROM MATCH_MAKING WHERE id = %s',(player_id,))
        player_details = []
        for i in self.cursor:
            player_details = i

        if player_details[-1] == "none":
            self.cursor.execute('SELECT id,player_power,level,room_id FROM MATCH_MAKING')
            all_players = []
            for i in self.cursor:
               all_players.append(list(i))

            target_players = []

            if len(all_players)-1==0:
                return "0"
            else:
                level_diff = 0
                power_level_diff = 0
                while True:

                    for i in all_players:
                        if  math.fabs(float(i[2]) - float(player_details[1]) <=float(level_diff)) or math.fabs(float(i[1])-float(player_details[0]) <=float(power_level_diff)):

                            if (i[-1] == "none" and i[0] != player_id):
                                target_players.append(i)



                    if (len(target_players)>=1):
                        self.remove_player(player_id)
                        return self.create_room(player_id,target_players[0][0])
                        break

                    else:
                        level_diff = level_diff+1
                        power_level_diff = power_level_diff + 1

        else:
            self.remove_player(player_id)
            return player_details[-1]




    def generate_unique_id(self):
        unique_id = ''
        for i in range(random.randrange(8,15)):
            unique_id = unique_id + random.choice(string.printable[:61])

        return unique_id



    def create_room(self,player1,player2):
        room_id = self.generate_unique_id()
        self.cursor.execute('UPDATE MATCH_MAKING SET room_id = %s WHERE id = %s', (room_id,player1))
        self.cursor.execute('UPDATE MATCH_MAKING SET room_id = %s WHERE id = %s', (room_id, player2))
        self.db.commit()

        self.match_cursor.execute("CREATE TABLE " + room_id+" ( playerid VARCHAR(2050) , player_stats VARCHAR(1000), player_coords VARCHAR(1000), minions_coords VARCHAR(1000), minions_stats VARCHAR(1000))")
        self.match_cursor.execute("INSERT INTO  " + room_id+ " (  playerid , player_stats , player_coords  , minions_coords , minions_stats) VALUES (%s ,%s ,%s ,%s ,%s  ) " , (player1 ,"0","0","0","0"))
        self.match_cursor.execute("INSERT INTO  " + room_id + " (  playerid , player_stats , player_coords  , minions_coords , minions_stats) VALUES (%s ,%s ,%s ,%s ,%s  ) ",(player2, "0", "0", "0", "0"))
        self.matches_db.commit()

        return room_id


    def get_opp_id(self,room_n,player_id):
        self.match_cursor.execute("SELECT playerid FROM "+room_n)
        id = ""
        for i in self.match_cursor:
            if i != player_id:
                id = i

        return id

    def delete_the_room(self,room_n):
        self.match_cursor.execute("DROP TABLE " + room_n)
        self.matches_db.commit()

    def update_player_details(self,data,room_n):


        self.match_cursor.execute("UPDATE " +room_n+ " SET player_stats = %s  WHERE playerid = %s",(data[0],data[2]))
        self.match_cursor.execute("UPDATE " + room_n + " SET player_coords = %s  WHERE playerid = %s", (data[1],data[2]))
        self.matches_db.commit()

    def get_data_to_render(self,room,player_id):
        self.match_cursor.execute("SELECT player_coords , player_stats FROM "+ room +" WHERE playerid = %s" , (player_id,))

        data = ""
        for i in self.match_cursor:
            data = i

        return data



