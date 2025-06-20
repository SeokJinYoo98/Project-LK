
using UnityEngine;
using GameSystem.MVPC;
using System.Collections.Generic;
using System.Xml.Linq;

public class StateMachine<T> where T : IPresenter
{

    private State<T>                      _mainState = null;
    private Dictionary<string, State<T>>  _subStates = new( );

    public void ChangeMainState(State<T> newState)
    {
        if (_mainState?.GetType( ) == newState.GetType( ))
            return;

        _mainState?.Exit();
        _mainState = newState;
        _mainState?.Enter();
    }

    public void AddSubState(State<T> state)
    {
        if (state == null || string.IsNullOrEmpty( state.Name ))
            return;
        if (_subStates.TryGetValue( state.Name, out var target ))
        {
            // 레벨업 에이블 변환 가능?
            // 가능 -> 레벨업 호출
            // 불가능 -> 타이머 초기화
        }
        else
        {
            _subStates[state.Name] = state;
            state.Enter( );
        }
        Debug.Log( state.Name + "SubState Added" );
    }
    public void RemoveSubState(State<T> state)
    {
        if (state == null || string.IsNullOrEmpty( state.Name ))
            return;

        if (_subStates.TryGetValue( state.Name, out var target ))
        {
            target.Exit( );  // 종료 처리 먼저
            _subStates.Remove( state.Name );  // 딕셔너리에서 제거
        }

    }
    public virtual void Execute(float deltaTime)
    {
        ExecuteMainState( deltaTime );
        ExecuteSubStates( deltaTime );
    }
    protected void ExecuteMainState(float deltaTime)
        => _mainState?.Execute( deltaTime );
    protected void ExecuteSubStates(float deltaTime)
    {
        foreach (var subState in _subStates.Values)
            subState.Execute( deltaTime );
    }
}