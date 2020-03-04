/// 
/// This file contains classes for all level states
/// 

public abstract class StateBase { }

public class PreparationState : StateBase { }
public class PatrolingState : StateBase { }
public class AttackState : StateBase { }
public class DefeatState : StateBase { }
public class WinState : StateBase { }