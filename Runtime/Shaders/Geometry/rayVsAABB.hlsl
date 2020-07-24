// lightly modified from https://tavianator.com/cgit/dimension.git/tree/libdimension/bvh/bvh.c#n196
// explanation of algorithm: https://tavianator.com/fast-branchless-raybounding-box-intersections/

// .x0      ray start
// .n_inv   inverse of each component of the ray's slope
//     .n_inv.x = 1.0 / ray.n.x

void rayVsAABB_float(float3 rayStart, float3 rayDirection, float3 boxMin, float3 boxMax,
    out bool hasIntersect, out float time, out float tmin, out float tmax)
{
    // This is actually correct, even though it appears not to handle edge cases
    // (ray.n.{x,y,z} == 0).  It works because the infinities that result from
    // dividing by zero will still behave correctly in the comparisons.  Rays
    // which are parallel to an axis and outside the box will have tmin == inf
    // or tmax == -inf, while rays inside the box will have tmin and tmax
    // unchanged.

    // if testing same aganist many AABB should make a version of function that
    // takes n_inv as input.
    float3 n_inv = float3(
        1.0 / rayDirection.x,
        1.0 / rayDirection.y,
        1.0 / rayDirection.z
    );

    float tx1 = (boxMin.x - rayStart.x) * n_inv.x;
    float tx2 = (boxMax.x - rayStart.x) * n_inv.x;

    tmin = min(tx1, tx2);
    tmax = max(tx1, tx2);

    float ty1 = (boxMin.y - rayStart.y) * n_inv.y;
    float ty2 = (boxMax.y - rayStart.y) * n_inv.y;

    tmin = max(tmin, min(ty1, ty2));
    tmax = min(tmax, max(ty1, ty2));

    float tz1 = (boxMin.z - rayStart.z) * n_inv.z;
    float tz2 = (boxMax.z - rayStart.z) * n_inv.z;

    tmin = max(tmin, min(tz1, tz2));
    tmax = min(tmax, max(tz1, tz2));

    // tmin is "earliest" ray is within bounds of box, which may be < 0 if ray starts in box
    
    // time will be when forward ray touches bounds of box, only valid if hasIntersect true
    time = tmin < 0 ? tmax : tmin;
    hasIntersect = tmax >= max(0.0, tmin);
    // hasIntersect = (tmax >= max(0.0, tmin) && tmin < maxTime);
}
