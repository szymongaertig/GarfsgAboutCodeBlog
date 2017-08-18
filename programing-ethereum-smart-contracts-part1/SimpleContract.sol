pragma solidity^0.4.0;

contract SimpleContract {
    uint _someContractValue;
    
    function getSomeContractValue() constant returns(uint){
        return _someContractValue;
    }
    
    function setSomeContractValue(uint newValue){
        _someContractValue = newValue;
    }
}