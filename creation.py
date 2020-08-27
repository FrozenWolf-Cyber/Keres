import mysql.connector

class create_db:

    def __init__(self,host,user,passwd,database):
        self.db = mysql.connector.connect(host=host,
                                     user=user,
                                     passwd=passwd,
                                     database=database)

        self.cursor = self.db.cursor()

        self.cursor.execute('''CREATE TABLE IF NOT EXISTS MATCH_MAKING (
                          id VARCHAR(20) ,
                          level VARCHAR(100) ,
                          player_power VARCHAR(1000) ,
                          room_id VARCHAR(1000) ,
                          PRIMARY KEY (id));
        
        ''')

        self.cursor.execute('''CREATE TABLE IF NOT EXISTS PLAYER_INFO (
                          id VARCHAR(20) UNIQUE ,
                          name VARCHAR(20) NOT NULL , 
                          DOB VARCHAR(20) ,
                          games_won VARCHAR(20) NOT NULL , 
                          games_lost VARCHAR(20) NOT NULL, 
                          games_drawn VARCHAR(20) NOT NULL , 
                          level VARCHAR(20) NOT NULL , 
                          exp VARCHAR(10) NOT NULL , 
                          badges VARCHAR(20) NOT NULL, 
                          user_name VARCHAR(20) NOT NULL,
                          monsters_slayed VARCHAR(100) NOT NULL,
                          gold VARCHAR(100) NOT NULL,
                          diamond VARCHAR(100) NOT NULL,
                          chest_info VARCHAR(1000) NOT NULL,
                          emotes_owned VARCHAR(1000) NOT NULL,
                          state VARCHAR(50) NOT NULL,
                          PRIMARY KEY (id));
        ''')

        self.cursor.execute('''CREATE TABLE IF NOT EXISTS PLAYER_LOGIN ( 
                          mail_id VARCHAR(30) NOT NULL ,
                          user_name VARCHAR(20) UNIQUE ,
                          password VARCHAR(20) NOT NULL ,
                          id VARCHAR(20) UNIQUE ,
                          PRIMARY KEY (mail_id));
        ''')

        self.db.commit()