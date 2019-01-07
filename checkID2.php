<?php

	$host = "localhost";
	$username = "id4770849_workceo";
	$password = "delabEGO234";
	$db_name = "id4770849_work";
	$con = mysqli_connect( "$host", "$username", "$password", "$db_name" ) or die("cannot connect");
	
    $data2 = $_POST[ 'ID' ];
    
    $sql = "SELECT Name, idnumber , Price FROM workHos WHERE idnumber = '".$data2."'";
    
    $result = mysqli_query( $con , $sql )
    or die ( "Error" );
    
    $json = array();
    
    if( mysqli_num_rows($result)){
    while($row = mysqli_fetch_row($result)){
    $json[] = $row;
    }
    }
    mysqli_close($con);
    $result = array();
    
    if (count($json) == 0 ){
        $result[0] = "Nothing";
    }else{
        $result[0] = "Something";
    }
    $result[1] = $json;
    echo ( json_encode( $result ) );

?>