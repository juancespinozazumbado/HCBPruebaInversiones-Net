---- INVERSIONES

1.-----------------------------------------------------------------------------
  CREATE TABLE "PRUEBA"."HCB_INVERSION" SHARING=METADATA 
   (	"MONTO_INVERSION" NUMBER DEFAULT 0 NOT NULL ENABLE, 
	"ID_INVERSION" NUMBER NOT NULL ENABLE, 
	"TASA_INT_ANUAL" FLOAT(126) NOT NULL ENABLE, 
	"PLAZO_MESES" NUMBER NOT NULL ENABLE, 
	"CUPONES_POR_AÑO" NUMBER NOT NULL ENABLE, 
	 CONSTRAINT "HCB_INVERSION_PK" PRIMARY KEY ("ID_INVERSION")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
2.-----------------------------------------------------------------------

CREATE SEQUENCE  "PRUEBA"."HCB_INVERSION_SEQ"  MINVALUE 1 MAXVALUE 9999 INCREMENT BY 1 START WITH 21 CACHE 20 ORDER  NOCYCLE  NOKEEP  NOSCALE  GLOBAL ;
3.-----------------------------------------------------------------------


  CREATE OR REPLACE NONEDITIONABLE TRIGGER "PRUEBA"."HCB_INVERSION_TRG" 
BEFORE INSERT ON PRUEBA.HCB_INVERSION
FOR EACH ROW
BEGIN
  IF :NEW.ID_INVERSION IS NULL THEN
    :NEW.ID_INVERSION := PRUEBA.HCB_INVERSION_SEQ.NEXTVAL;
  END IF;
END;
/
ALTER TRIGGER "PRUEBA"."HCB_INVERSION_TRG" ENABLE;



---- INVERSION_DETALLE


1.-----------------------------------------------------------
CREATE TABLE HCB_DETALLE_INVERSION 
(
  ID_DETALLE NUMBER NOT NULL 
, AÑO NUMBER DEFAULT 0 NOT NULL 
, CUPON NUMBER NOT NULL 
, SALDO_INVERSION NUMBER DEFAULT 0 NOT NULL 
, INTERES_GANADO NUMBER NOT NULL 
, SALDO_CAPITALIZADO NUMBER DEFAULT 0 NOT NULL 
, ID_INVERSION NUMBER NOT NULL 
, CONSTRAINT HCB_DETALLE_INVERSION_PK PRIMARY KEY 
  (
    ID_DETALLE 
  )
  USING INDEX 
  (
      CREATE UNIQUE INDEX HCB_DETALLE_INVERSION_PK ON HCB_DETALLE_INVERSION (ID_DETALLE ASC) 
      LOGGING 
      TABLESPACE USERS 
      PCTFREE 10 
      INITRANS 2 
      STORAGE 
      ( 
        INITIAL 65536 
        NEXT 1048576 
        MINEXTENTS 1 
        MAXEXTENTS UNLIMITED 
        BUFFER_POOL DEFAULT 
      ) 
      NOPARALLEL 
  )
  ENABLE 
) 
LOGGING 
TABLESPACE USERS 
PCTFREE 10 
INITRANS 1 
STORAGE 
( 
  INITIAL 65536 
  NEXT 1048576 
  MINEXTENTS 1 
  MAXEXTENTS UNLIMITED 
  BUFFER_POOL DEFAULT 
) 
NOCOMPRESS 
NO INMEMORY 
NOPARALLEL;

 2.--------------------------------------------------------------------- 

CREATE SEQUENCE  "PRUEBA"."HCB_DETALLE_INVERSION_SEQ"  MINVALUE 1 MAXVALUE 9999 INCREMENT BY 1 START WITH 281 CACHE 20 ORDER  NOCYCLE  NOKEEP  NOSCALE  GLOBAL ;





create or replace NONEDITIONABLE TRIGGER PRUEBA. HCB_DETALLE_INVERSION_TRG
BEFORE INSERT ON PRUEBA.HCB_DETALLE_INVERSION 
FOR EACH ROW
BEGIN
  IF :NEW.ID_DETALLE IS NULL THEN
    :NEW.ID_DETALLE := PRUEBA.HCB_DETALLE_INVERSION_SEQ.NEXTVAL;
  END IF;
END;


3-----------------------------------------------------------------


----- ENCABEZADOS

CREATE TABLE HCB_ENCABEZADO_INVERSION 
(
  ID_ENCABEZADO NUMBER NOT NULL 
, ID_INVERSION NUMBER NOT NULL 
, TOTAL_INTERES NUMBER DEFAULT 0 NOT NULL 
, SALDO_CAPITALIZADO NUMBER DEFAULT 0 NOT NULL 
) 
LOGGING 
TABLESPACE USERS 
PCTFREE 10 
INITRANS 1 
STORAGE 
( 
  INITIAL 65536 
  NEXT 1048576 
  MINEXTENTS 1 
  MAXEXTENTS UNLIMITED 
  BUFFER_POOL DEFAULT 
) 
NOCOMPRESS 
NO INMEMORY 
NOPARALLEL;


CREATE SEQUENCE  "PRUEBA"."HCB_ENCABEZADO_SEQ"  MINVALUE 1 MAXVALUE 9999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE  GLOBAL ;

create or replace NONEDITIONABLE TRIGGER PRUEBA.HCB_ENCABEZADO_TRG
BEFORE INSERT ON PRUEBA.HCB_ENCABEZADO_INVERSION
FOR EACH ROW
BEGIN
  IF :NEW.ID_ENCABEZADO IS NULL THEN
    :NEW.ID_ENCABEZADO := PRUEBA.HCB_ENCABEZADO_SEQ.NEXTVAL;
  END IF;
END;








