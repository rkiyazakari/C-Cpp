INSERT INTO public."User" ("UserId", "Username", "Password") VALUES (1, 'thibault', '$2y$12$1gY85UsgQmogHcCJ0R9znuF/M61JNk.fdFKxPUJVHGw7FJjxlCRUG');
INSERT INTO public."User" ("UserId", "Username", "Password") VALUES (2, 'Ruben', '$2y$12$Q.Yh/mvJ6LI7qQz2kmhLeOIM4CWw/E9Z3u.HDdVX0ky0d7Coi.hly');

INSERT INTO public."Topics" ("TopicsId", "Title", "Description") VALUES (1, 'CS:GO', 'Game topic about CS:GO pew pew');
INSERT INTO public."Topics" ("TopicsId", "Title", "Description") VALUES (2, 'Minecraft', null);

INSERT INTO public."Connect" ("UserId", "TopicsId") VALUES (1, 1);
INSERT INTO public."Connect" ("UserId", "TopicsId") VALUES (2, 2);
INSERT INTO public."Connect" ("UserId", "TopicsId") VALUES (1, 2);

INSERT INTO public."TopicMessage" ("TopicMessageId", "CreatedAt", "Text", "UserId", "TopicsId") VALUES (1, '2021-01-10 15:22:33.904605', 'test on minecraft Topic', 2, 2);
INSERT INTO public."TopicMessage" ("TopicMessageId", "CreatedAt", "Text", "UserId", "TopicsId") VALUES (2, '2021-01-10 15:23:06.593827', 'test 2 on minecraft Topic', 1, 2);

INSERT INTO public."DirectMessage" ("DirectMessageId", "Text", "CreatedAt", "Sender", "Receiver") VALUES (1, 'hello', '15:13:41.293118 +01:00', 1, 2);
INSERT INTO public."DirectMessage" ("DirectMessageId", "Text", "CreatedAt", "Sender", "Receiver") VALUES (2, 'hello', '15:14:24.311631 +01:00', 1, 2);
