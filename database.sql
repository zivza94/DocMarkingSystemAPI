--------------------------------------------------------
--  File created - Thursday-October-01-2020   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table DOCUMENTS
--------------------------------------------------------

  CREATE TABLE "ZIVPROJ"."DOCUMENTS" 
   (	"USER_ID" VARCHAR2(40 BYTE), 
	"IMAGE_URL" VARCHAR2(100 BYTE), 
	"DOCUMENT_NAME" VARCHAR2(20 BYTE), 
	"DOCUMENT_ID" VARCHAR2(36 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table DOCUMENTS_MARKERS
--------------------------------------------------------

  CREATE TABLE "ZIVPROJ"."DOCUMENTS_MARKERS" 
   (	"DOC_ID" VARCHAR2(36 BYTE), 
	"MARKER_ID" VARCHAR2(36 BYTE), 
	"MARKER_TYPE" VARCHAR2(10 BYTE), 
	"MARKER_X" NUMBER(4,0), 
	"MARKER_Y" NUMBER(4,0), 
	"MARKER_X_RADIUS" NUMBER(4,0), 
	"MARKER_Y_RADIUS" NUMBER(4,0), 
	"FORE_COLOR" VARCHAR2(20 BYTE), 
	"BACK_COLOR" VARCHAR2(20 BYTE), 
	"USER_ID" VARCHAR2(40 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table SHARED_DOCUMENTS
--------------------------------------------------------

  CREATE TABLE "ZIVPROJ"."SHARED_DOCUMENTS" 
   (	"DOC_ID" VARCHAR2(36 BYTE), 
	"USER_ID" VARCHAR2(40 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table USERS
--------------------------------------------------------

  CREATE TABLE "ZIVPROJ"."USERS" 
   (	"USERID" VARCHAR2(40 BYTE), 
	"USERNAME" VARCHAR2(20 BYTE), 
	"REMOVED" NUMBER(1,0) DEFAULT 0
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
REM INSERTING into ZIVPROJ.DOCUMENTS
SET DEFINE OFF;
REM INSERTING into ZIVPROJ.DOCUMENTS_MARKERS
SET DEFINE OFF;
REM INSERTING into ZIVPROJ.SHARED_DOCUMENTS
SET DEFINE OFF;
REM INSERTING into ZIVPROJ.USERS
SET DEFINE OFF;
--------------------------------------------------------
--  DDL for Index DOCUMENTS_MARKERS_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "ZIVPROJ"."DOCUMENTS_MARKERS_PK" ON "ZIVPROJ"."DOCUMENTS_MARKERS" ("DOC_ID", "MARKER_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index DOCUMENTS_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "ZIVPROJ"."DOCUMENTS_PK" ON "ZIVPROJ"."DOCUMENTS" ("DOCUMENT_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SHARED_DOCUMENTS_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "ZIVPROJ"."SHARED_DOCUMENTS_PK" ON "ZIVPROJ"."SHARED_DOCUMENTS" ("USER_ID", "DOC_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index USERS_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "ZIVPROJ"."USERS_PK" ON "ZIVPROJ"."USERS" ("USERID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Procedure CREATEDOCUMENT
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "ZIVPROJ"."CREATEDOCUMENT" 
(
  P_DOCUMENT_ID IN VARCHAR2 
, P_USER_ID IN VARCHAR2 
, P_IMAGE_URL IN VARCHAR2 
, P_DOCUMENT_NAME IN VARCHAR2 
) AS 
BEGIN
  insert into documents (user_id,image_url,document_name,document_id)
  values(P_USER_ID,P_IMAGE_URL,P_DOCUMENT_NAME,P_DOCUMENT_ID);
END CREATEDOCUMENT;

/
--------------------------------------------------------
--  DDL for Procedure CREATEMARKER
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "ZIVPROJ"."CREATEMARKER" 
(
  P_DOC_ID IN VARCHAR2 
, P_MARKER_ID IN VARCHAR2 
, P_MARKER_TYPE IN VARCHAR2 
, P_MARKER_X IN NUMBER 
, P_MARKER_Y IN NUMBER 
, P_MARKER_X_RADIUS IN NUMBER 
, P_MARKER_Y_RADIUS IN NUMBER 
, P_FORE_COLOR IN VARCHAR2 
, p_BACK_COLOR IN VARCHAR2 
, P_USER_ID IN VARCHAR2 
) AS 
BEGIN
  insert into documents_markers(doc_id,marker_id,marker_type,marker_x,marker_y,marker_x_radius,marker_y_radius,fore_color,back_color,user_id)
  values (P_DOC_ID,P_MARKER_ID,P_MARKER_TYPE,P_MARKER_X,P_MARKER_Y,P_MARKER_X_RADIUS,P_MARKER_Y_RADIUS,P_FORE_COLOR,p_BACK_COLOR,P_USER_ID);
END CREATEMARKER;

/
--------------------------------------------------------
--  DDL for Procedure CREATESHARE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "ZIVPROJ"."CREATESHARE" 
(
  P_DOC_ID IN VARCHAR2 
, P_USER_ID IN VARCHAR2 
) AS 
BEGIN
  insert into shared_documents(doc_id,user_id) values(P_DOC_ID,P_USER_ID);
END CREATESHARE;

/
--------------------------------------------------------
--  DDL for Procedure CREATEUSER
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "ZIVPROJ"."CREATEUSER" 
(
  P_USERID IN VARCHAR2 
, P_USERNAME IN VARCHAR2 
) AS 
BEGIN
  insert into USERS (USERID,USERNAME) values (P_USERID,P_USERNAME);
END CREATEUSER;

/
--------------------------------------------------------
--  DDL for Procedure GETDOCUMENT
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "ZIVPROJ"."GETDOCUMENT" 
(
  P_DOC_ID IN VARCHAR2 
, P_RETVAL OUT SYS_REFCURSOR 
) AS 
BEGIN
  open P_RETVAL for
  select * from documents where document_id = P_DOC_ID;
END GETDOCUMENT;

/
--------------------------------------------------------
--  DDL for Procedure GETDOCUMENTS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "ZIVPROJ"."GETDOCUMENTS" 
(
  P_USERID IN VARCHAR2 
, P_RETVAL OUT SYS_REFCURSOR 
) AS 
BEGIN
  open P_RETVAL for
  select * from documents where USER_ID = P_USERID;
END GETDOCUMENTS;

/
--------------------------------------------------------
--  DDL for Procedure GETMARKERS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "ZIVPROJ"."GETMARKERS" 
(
  P_DOC_ID IN VARCHAR2 
, P_RETVAL OUT SYS_REFCURSOR 
) AS 
BEGIN
  open P_RETVAL for
  select * from documents_markers where doc_id = P_DOC_ID;
END GETMARKERS;

/
--------------------------------------------------------
--  DDL for Procedure GETSHAREDDOCUMENTS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "ZIVPROJ"."GETSHAREDDOCUMENTS" 
(
  P_USER_ID IN VARCHAR2 ,
  P_RETVAL OUT SYS_REFCURSOR
) AS 
BEGIN
  open p_retval for
  select * from shared_documents where user_id = P_USER_ID;
END GETSHAREDDOCUMENTS;

/
--------------------------------------------------------
--  DDL for Procedure GETSHAREDUSERS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "ZIVPROJ"."GETSHAREDUSERS" 
(
  P_DOC_ID IN VARCHAR2 
, P_RETVAL OUT SYS_REFCURSOR 
) AS 
BEGIN
  open p_retval for
  select * from shared_documents where doc_id = P_DOC_ID;
END GETSHAREDUSERS;

/
--------------------------------------------------------
--  DDL for Procedure LOGIN
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "ZIVPROJ"."LOGIN" 
(
  P_USERID IN NVARCHAR2
, P_retval OUT SYS_REFCURSOR 
) AS 
BEGIN
 open P_retval FOR
 select * FROM ZIVPROJ.USERS where userID = P_UserID and removed = 0;
END LOGIN;

/
--------------------------------------------------------
--  DDL for Procedure REMOVEDOCUMENT
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "ZIVPROJ"."REMOVEDOCUMENT" 
(
  P_DOC_ID IN VARCHAR2 
) AS 
BEGIN
    delete from documents_markers where doc_id = P_DOC_ID;
    delete from shared_documents where doc_id = P_DOC_ID;
    delete from documents where document_id = P_DOC_ID;
END REMOVEDOCUMENT;

/
--------------------------------------------------------
--  DDL for Procedure REMOVEMARKER
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "ZIVPROJ"."REMOVEMARKER" 
(
  P_MARKER_ID IN VARCHAR2 
) AS 
BEGIN
  delete from documents_markers where marker_id = P_MARKER_ID;
END REMOVEMARKER;

/
--------------------------------------------------------
--  DDL for Procedure REMOVESHARE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "ZIVPROJ"."REMOVESHARE" 
(
  P_DOC_ID IN VARCHAR2 
, P_USER_ID IN VARCHAR2 
) AS 
BEGIN
  delete from shared_documents where doc_id = P_DOC_ID and user_id = p_user_id;
END REMOVESHARE;

/
--------------------------------------------------------
--  DDL for Procedure REMOVEUSER
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "ZIVPROJ"."REMOVEUSER" 
(
  P_USERID IN VARCHAR2 
) AS 
BEGIN
  UPDATE USERS
  SET
    removed = 1
  WHERE
    userid = P_USERID;
END REMOVEUSER;

/
--------------------------------------------------------
--  Constraints for Table SHARED_DOCUMENTS
--------------------------------------------------------

  ALTER TABLE "ZIVPROJ"."SHARED_DOCUMENTS" MODIFY ("DOC_ID" NOT NULL ENABLE);
  ALTER TABLE "ZIVPROJ"."SHARED_DOCUMENTS" MODIFY ("USER_ID" NOT NULL ENABLE);
  ALTER TABLE "ZIVPROJ"."SHARED_DOCUMENTS" ADD CONSTRAINT "SHARED_DOCUMENTS_PK" PRIMARY KEY ("USER_ID", "DOC_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table DOCUMENTS
--------------------------------------------------------

  ALTER TABLE "ZIVPROJ"."DOCUMENTS" MODIFY ("DOCUMENT_ID" NOT NULL ENABLE);
  ALTER TABLE "ZIVPROJ"."DOCUMENTS" ADD CONSTRAINT "DOCUMENTS_PK" PRIMARY KEY ("DOCUMENT_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table USERS
--------------------------------------------------------

  ALTER TABLE "ZIVPROJ"."USERS" MODIFY ("USERID" NOT NULL ENABLE);
  ALTER TABLE "ZIVPROJ"."USERS" ADD CONSTRAINT "USERS_PK" PRIMARY KEY ("USERID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table DOCUMENTS_MARKERS
--------------------------------------------------------

  ALTER TABLE "ZIVPROJ"."DOCUMENTS_MARKERS" MODIFY ("DOC_ID" NOT NULL ENABLE);
  ALTER TABLE "ZIVPROJ"."DOCUMENTS_MARKERS" MODIFY ("MARKER_ID" NOT NULL ENABLE);
  ALTER TABLE "ZIVPROJ"."DOCUMENTS_MARKERS" ADD CONSTRAINT "DOCUMENTS_MARKERS_PK" PRIMARY KEY ("DOC_ID", "MARKER_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table DOCUMENTS
--------------------------------------------------------

  ALTER TABLE "ZIVPROJ"."DOCUMENTS" ADD CONSTRAINT "DOCUMENTS_FK_USER_ID" FOREIGN KEY ("USER_ID")
	  REFERENCES "ZIVPROJ"."USERS" ("USERID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table DOCUMENTS_MARKERS
--------------------------------------------------------

  ALTER TABLE "ZIVPROJ"."DOCUMENTS_MARKERS" ADD CONSTRAINT "DOCUMENTS_MARKERS_FK_DOC_ID" FOREIGN KEY ("DOC_ID")
	  REFERENCES "ZIVPROJ"."DOCUMENTS" ("DOCUMENT_ID") ENABLE;
  ALTER TABLE "ZIVPROJ"."DOCUMENTS_MARKERS" ADD CONSTRAINT "DOCUMENTS_MARKERS_FK_USER_ID" FOREIGN KEY ("USER_ID")
	  REFERENCES "ZIVPROJ"."USERS" ("USERID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table SHARED_DOCUMENTS
--------------------------------------------------------

  ALTER TABLE "ZIVPROJ"."SHARED_DOCUMENTS" ADD CONSTRAINT "SHARED_DOCUMENTS_FK1" FOREIGN KEY ("DOC_ID")
	  REFERENCES "ZIVPROJ"."DOCUMENTS" ("DOCUMENT_ID") ENABLE;
  ALTER TABLE "ZIVPROJ"."SHARED_DOCUMENTS" ADD CONSTRAINT "SHARED_DOCUMENTS_FK_USER_ID" FOREIGN KEY ("USER_ID")
	  REFERENCES "ZIVPROJ"."USERS" ("USERID") ENABLE;
