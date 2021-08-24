-- *************** SqlDBM: PostgreSQL ****************;
-- ***************************************************;


-- ************************************** "User"

CREATE TABLE "User"
(
 "UserId"   serial NOT NULL,
 "Username" varchar(50) NOT NULL,
 "Password" varchar(100) NOT NULL,
 CONSTRAINT "PK_user" PRIMARY KEY ( "UserId" )
);








-- ************************************** "Topics"

CREATE TABLE "Topics"
(
 "TopicsId"    serial NOT NULL,
 "Title"       varchar(50) NOT NULL,
 "Description" text NULL,
 CONSTRAINT "PK_topics" PRIMARY KEY ( "TopicsId" )
);








-- ************************************** "TopicMessage"

CREATE TABLE "TopicMessage"
(
 "TopicMessageId" serial NOT NULL,
 "CreatedAt"      timestamp with time zone NOT NULL,
 "Text"           text NOT NULL,
 "UserId"         integer NOT NULL,
 "TopicsId"       integer NOT NULL,
 CONSTRAINT "PK_topicmessage" PRIMARY KEY ( "TopicMessageId" ),
 CONSTRAINT "FK_26" FOREIGN KEY ( "UserId" ) REFERENCES "User" ( "UserId" ),
 CONSTRAINT "FK_29" FOREIGN KEY ( "TopicsId" ) REFERENCES "Topics" ( "TopicsId" )
);

CREATE INDEX "fkIdx_26" ON "TopicMessage"
(
 "UserId"
);

CREATE INDEX "fkIdx_29" ON "TopicMessage"
(
 "TopicsId"
);








-- ************************************** "DirectMessage"

CREATE TABLE "DirectMessage"
(
 "DirectMessageId" serial NOT NULL,
 "Text"            text NOT NULL,
 "CreatedAt"       time with time zone NOT NULL,
 "Sender"          integer NOT NULL,
 "Receiver"        integer NOT NULL,
 CONSTRAINT "PK_directmessage" PRIMARY KEY ( "DirectMessageId" ),
 CONSTRAINT "FK_37" FOREIGN KEY ( "Sender" ) REFERENCES "User" ( "UserId" ),
 CONSTRAINT "FK_40" FOREIGN KEY ( "Receiver" ) REFERENCES "User" ( "UserId" )
);

CREATE INDEX "fkIdx_37" ON "DirectMessage"
(
 "Sender"
);

CREATE INDEX "fkIdx_40" ON "DirectMessage"
(
 "Receiver"
);








-- ************************************** "Connect"

CREATE TABLE "Connect"
(
 "UserId"   integer NOT NULL,
 "TopicsId" integer NOT NULL,
 CONSTRAINT "PK_connect" PRIMARY KEY ( "UserId", "TopicsId" ),
 CONSTRAINT "FK_14" FOREIGN KEY ( "UserId" ) REFERENCES "User" ( "UserId" ),
 CONSTRAINT "FK_18" FOREIGN KEY ( "TopicsId" ) REFERENCES "Topics" ( "TopicsId" )
);

CREATE INDEX "fkIdx_14" ON "Connect"
(
 "UserId"
);

CREATE INDEX "fkIdx_18" ON "Connect"
(
 "TopicsId"
);







