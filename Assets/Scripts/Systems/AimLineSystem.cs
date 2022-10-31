using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class AimLineSystem : IInit, IUpd
    {
        Filter<InputData> inputFilter = null;
        SceneData _scene = null;
        StaticData _stData = null;

        int _reflectMask;
        LineRenderer _lineRenderer;
        MyList<Vector3> points = new MyList<Vector3>(4);

        public void Init()
        {
            _reflectMask = _stData.ReflectMask;
            _lineRenderer = _scene.AimLine;
        }

        public void Upd()
        {
            //Сброс позиции AimBall
            _scene.AimBall.position = _stData.LimboPos;

            foreach (var i in inputFilter)
            {
                var ent = inputFilter.GetEntity(i);
                ref var input = ref inputFilter.Get1(i);
                Vector2 firePointPos = _scene.FirePoint.position;

                //AimLine
                if (input.IsInsideFireZone & input.IsPressed & !ent.Contain<BlockInputTag>())
                {
                    if (!_lineRenderer.enabled) _lineRenderer.enabled = true;
                    DrawAimLine(firePointPos, input.pos - firePointPos);
                }
                else
                {
                    if (_lineRenderer.enabled) _lineRenderer.enabled = false;
                }
            }
        }

        void DrawAimLine(Vector2 origin, Vector2 direction)
        {
            points.Clear();
            points.Add(origin);

            for (int i = 0, iMax = _stData.AimCastCount; i < iMax; i++)
            {
                var hit = Physics2D.CircleCast(origin, 0.5f, direction, float.PositiveInfinity, _reflectMask);
                if (!hit || hit.collider.CompareTag("Void")) break;

                var isLastRaycast = i == iMax - 1;
                var isBigDistannce = hit.distance > _stData.LastAimRange;

                if (isLastRaycast & isBigDistannce)
                {
                    points.Add(origin + (hit.centroid - origin).normalized * _stData.LastAimRange);
                }
                else
                {
                    points.Add(hit.centroid);

                    if (hit.collider.CompareTag("Ball"))
                    {
                        DrawAimBall(hit);
                        break;
                    }
                }

                direction = Vector2.Reflect(hit.centroid - origin, hit.normal).normalized;
                origin = hit.centroid + direction * 0.05f;
            }

            _lineRenderer.positionCount = points.Count;
            _lineRenderer.SetPositions(points.ToArray());
        }

        void DrawAimBall(RaycastHit2D hit)
        {
            Vector3 aimBallPos = BallHelper.GetBallPositionByHit(hit);
            aimBallPos.z = -1;
            _scene.AimBall.position = aimBallPos;
        }
    }
}