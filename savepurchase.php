
<?php
	$databaseAmt = $_POST["Amt"];
	$dbID = $_POST["ID"];	

	$host = "localhost";
	$username = "id4770849_workceo";
	$password = "delabEGO234";
	$db_name = "id4770849_work";
	$con = mysqli_connect( "$host", "$username", "$password", "$db_name" ) or die("cannot connect");
	
	$databaseName = mysqli_real_escape_string( $con, $databaseName );
    
    $sql = "SELECT price FROM workHos WHERE idnumber = '".$dbID."'";
    
    $result = mysqli_query( $con , $sql )
    or die ( "Error" );
    
    $json = array();
    
    if( mysqli_num_rows($result)){
    while($row = mysqli_fetch_row($result)){
    $json[] = $row;
    }
    }
    
    $databaseAmt = $json[0][0] + $databaseAmt;
    
	$sql = "UPDATE workHos SET price = '".$databaseAmt."' WHERE idnumber = '".$dbID."'";
	$result = mysqli_query( $con , $sql )
	or die ("Error");	
	mysqli_close($con); 
	echo("SAVED");
	
?>