create or replace PACKAGE BODY PCK_HCB_INVERSIONES AS

  PROCEDURE AGREGAR_INVERSION(
    monto number, 
    tasa_interes Decimal,
    plazo number,
    cupones_anual NUMBER) AS
  BEGIN
    
     INSERT INTO hcb_inversion (MONTO_INVERSION, TASA_INTERES_ANUAL,PLAZO_MESES, CANT_CUPONES_ANUAL)
      VALUES ( monto, tasa_interes, plazo, cupones_anual);
      COMMIT;
 
  END AGREGAR_INVERSION;

  PROCEDURE MODIFICAR_INVERSION(
    Id number,
    monto_inversion number, 
    tasa_interes_anual DECIMAL,
    plazo_meses number,
    cant_cupones_anual NUMBER) AS
  BEGIN
    -- TAREA: Se necesita implantación para PROCEDURE PCK_HCB_INVERSIONES.MODIFICAR_INVERSION
    NULL;
  END MODIFICAR_INVERSION;

  PROCEDURE ELIMINAR_INVERSION(Id_inversion number) AS
  BEGIN
    -- TAREA: Se necesita implantación para PROCEDURE PCK_HCB_INVERSIONES.ELIMINAR_INVERSION
    NULL;
  END ELIMINAR_INVERSION;

  PROCEDURE LISTAR_INVERSIONES( inversiones out SYS_REFCURSOR ) AS
  BEGIN
     OPEN inversiones FOR
       SELECT ID_INVERSION, MONTO_INVERSION, TASA_INTERES_ANUAL,PLAZO_MESES, CANT_CUPONES_ANUAL FROM 
      HCB_INVERSION;

  END LISTAR_INVERSIONES;

  PROCEDURE LISTAR_INVERSIONES_DETALLES(Id_inversion number,  inversionesDetalles out SYS_REFCURSOR)  AS
  BEGIN
     OPEN inversionesDetalles FOR 
       SELECT * FROM hcb_inversion_detalle WHERE id_inversion = Id_inversion; 
    NULL;
  END LISTAR_INVERSIONES_DETALLES;
  
  
  
  
   PROCEDURE LISTAR_ENCABEZADO (Id_inversion number,  Encabezado out SYS_REFCURSOR )AS
  BEGIN
     OPEN Encabezado FOR 
       SELECT * FROM hcb_inversion_encabezado WHERE id_inversion = Id_inversion ; 
  END LISTAR_ENCABEZADO;
   
   
   PROCEDURE GENERAR_CUPONES(inversion_id number, resultado out SYS_REFCURSOR)AS
   numero_cupones number;
   tasa number;
   interes FLOAT;
   saldo number;
   plazo number;
   saldo_capitalizado number;
   cantidad_cupones_totales number;
   
  BEGIN
      SELECT cant_cupones_anual INTO numero_cupones  from hcb_inversion;
      SELECT MONTO_INVERSION INTO saldo  from hcb_inversion;
      SELECT TASA_INTERES_ANUAL INTO tasa  from hcb_inversion;
       SELECT PLAZO_MESES INTO tasa  from hcb_inversion;
      
      
      FOR indice in 1..numero_cupones LOOP
       interes := saldo *(tasa/100/12)*(12/numero_cupones);
       saldo_capitalizado:= saldo+interes;
       
       INSERT INTO hcb_inversion_detalle(CUPON, SALDO_INVERSION, INTERESS_GANADOS, SALDO_CAPITALIZADO, ID_INVERSION)
       VALUES(indice, saldo, interes, saldo_capitalizado, inversion_id );
       
       END LOOP;
       
       -- modificar encabezado
      
       INSERT INTO hcb_inversion_encabezado (TOTAL_INTERES,SALDO_CAPITALIZDO_FINAL, ID_INVERSION )
        VALUES (interes,saldo_capitalizado, inversion_id );
        
       COMMIT;
      
      
  END GENERAR_CUPONES;
  
  

END PCK_HCB_INVERSIONES;