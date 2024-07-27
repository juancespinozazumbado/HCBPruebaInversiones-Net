create or replace NONEDITIONABLE PACKAGE BODY        HCB_MANEJA_INVERSIONES AS

  PROCEDURE AGREGAR_INVERSION( MontoInversion in number, TasaInteres double precision, PlazoMeses number, CuponesAnuales number) AS
  BEGIN
    INSERT INTO PRUEBA.HCB_INVERSION( MONTO_INVERSION,
    TASA_INT_ANUAL, PLAZO_MESES,  CUPONES_POR_AÑO
    ) VALUES (MontoInversion, TasaInteres, PlazoMeses,CuponesAnuales );
    
    --- Agregamos los encabezados en 0
    COMMIT;
  END AGREGAR_INVERSION;
  
  
  ------------------------------------------------------------------------------------------------------------------------------------
  PROCEDURE INSERTAR_DETALLE(IdInversion in number, Año in number, Cupon in number, 
  Saldo in number, InteresGanado in number, SaldoCapitalizado in number )AS
  BEGIN
     INSERT INTO PRUEBA.HCB_DETALLE_INVERSION(ID_INVERSION, AÑO, CUPON, SALDO_INVERSION, INTERES_GANADO, SALDO_CAPITALIZADO)
     VALUES(IdInversion, Año, Cupon, Saldo, InteresGanado, SaldoCapitalizado );
     COMMIT;
  
  END INSERTAR_DETALLE;
  ---------------------------------------------------------------------------------------------------------------------------------------------------------
   PROCEDURE INSERTAR_ENCABEZADO(IdInversion in number, InteresTotal in number, SaldoCapitalizado in number )AS
   BEGIN
        INSERT INTO PRUEBA.HCB_ENCABEZADO_INVERSION(ID_INVERSION,TOTAL_INTERES, SALDO_CAPITALIZADO )
        VALUES (IdInversion, InteresTotal,SaldoCapitalizado );
        COMMIT;
     NULL;
   END INSERTAR_ENCABEZADO;
  
  
----------------------------------------------------------------------------------------------------------------------

  PROCEDURE CALCULAR_CUPONES(IdInversion in  number, MensageError out  nvarchar2  ) AS
   NumeroCupones number;
   TasaInteres double precision;
   Interes number;
   Saldo number;
   Plazo number;
   SaldoCapitalizado number;
   TotalIteraciones number; --por calcular
   cupon number;
   Año number;
   iteracioon number;
   mesesPorCupon number;
   
  BEGIN
     --- Seteamos las variables
      SELECT MONTO_INVERSION, TASA_INT_ANUAL, PLAZO_MESES, CUPONES_POR_AÑO
      INTO Saldo, TasaInteres, Plazo, NumeroCupones FROM 
       PRUEBA.HCB_INVERSION WHERE ID_INVERSION = IdInversion;
       
        TotalIteraciones := Plazo * NumeroCupones /12; -- 
       -- mesesPorCupon := 12/NumeroCupones;
        Año := 1;
        iteracioon := NumeroCupones;
        
          WHILE TotalIteraciones > 0 LOOP
               FOR i IN 1..iteracioon LOOP
              
                  Interes := Saldo * (TasaInteres/100/12)*(12/NumeroCupones);
                  Interes:= Round(Interes, 0);
                  SaldoCapitalizado := Saldo + Interes;
                  INSERTAR_DETALLE(IdInversion,Año, i, Saldo, Interes, SaldoCapitalizado );
                 Saldo := SaldoCapitalizado;
                 
               END LOOP;
               
               TotalIteraciones:=  TotalIteraciones - NumeroCupones;
               iteracioon := TotalIteraciones;
               Año := Año + 1;
         END LOOP; 
        -- Actualiza el encabezado de esta inversion
         ACTUALIZAR_ENCABEZADOS(IdInversion, MensageError);
         COMMIT;

  END CALCULAR_CUPONES;
  
  
  ---------------------------------------------------------------------------------------------------------------

  PROCEDURE ACTUALIZAR_ENCABEZADOS(IdInversion in number, MensageError out nvarchar2 ) AS
   TotalIntereses number;
   SaldoFinal number;
   SaldoInicial number;
  BEGIN
     SELECT SUM(INTERES_GANADO) INTO TotalIntereses FROM PRUEBA.HCB_DETALLE_INVERSION
      WHERE ID_INVERSION = IdInversion;
    SELECT MONTO_INVERSION INTO SaldoInicial FROM PRUEBA.HCB_INVERSION WHERE
      ID_INVERSION = IdInversion;
      
     SaldoFinal := SaldoInicial + TotalIntereses;
     
     UPDATE PRUEBA.HCB_ENCABEZADO_INVERSION 
      SET TOTAL_INTERES = TotalIntereses , SALDO_CAPITALIZADO = SaldoFinal
      WHERE ID_INVERSION = IdInversion;
      COMMIT;
    
    NULL;
  END ACTUALIZAR_ENCABEZADOS;







--------LECTURA ------------------------------------------
  PROCEDURE LISTAR_INVERSIONES(Inversiones OUT SYS_REFCURSOR) AS
  BEGIN
    OPEN Inversiones FOR SELECT ID_INVERSION, MONTO_INVERSION, TASA_INT_ANUAL, PLAZO_MESES, CUPONES_POR_AÑO FROM 
     PRUEBA.HCB_INVERSION;

  END LISTAR_INVERSIONES;

  PROCEDURE LISTAR_DETALLES_INVERSION(IdInversion in number , Detalles OUT SYS_REFCURSOR) AS
  BEGIN
     OPEN Detalles FOR SELECT ID_DETALLE, ID_INVERSION,AÑO, CUPON, SALDO_INVERSION, INTERES_GANADO, SALDO_CAPITALIZADO, ID_INVERSION FROM 
     PRUEBA.HCB_DETALLE_INVERSION WHERE ID_INVERSION = IdInversion ;
   
  END LISTAR_DETALLES_INVERSION;

  PROCEDURE LISTAR_ENCABEZADOS_INVERSION(IdInversion in number , Encabezados OUT SYS_REFCURSOR) AS
  BEGIN
     OPEN Encabezados FOR SELECT ID_ENCABEZADO,ID_INVERSION, TOTAL_INTERES, SALDO_CAPITALIZADO FROM 
     PRUEBA.HCB_ENCABEZADO_INVERSION WHERE ID_INVERSION = IdInversion ;
    
  END LISTAR_ENCABEZADOS_INVERSION;


 PROCEDURE OBTENER_INVERSION(IdInversion in number , Inversion OUT SYS_REFCURSOR) AS
  BEGIN
     OPEN Inversion FOR SELECT ID_INVERSION, MONTO_INVERSION, TASA_INT_ANUAL, PLAZO_MESES, CUPONES_POR_AÑO FROM 
     PRUEBA.HCB_INVERSION WHERE ID_INVERSION = IdInversion ;
   
  END OBTENER_INVERSION;
  

END  HCB_MANEJA_INVERSIONES;