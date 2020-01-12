# put in enviroment variables
# export APP_NAME="boilerplate-dotnet"
# export REGISTRY="9.6.48.179:5000"
# export NAMESPACE="boilerplate-dotnet"
# export APPPORT="80"
# export IMAGE_VERSION="v01"
# export URL_PREFIX="api"
# export IMAGE_ID=$APP_NAME

# build

IMAGE_NAME="$REGISTRY/$IMAGE_ID:$IMAGE_VERSION"
echo "CREATING IMAGE $IMAGE_NAME"

sed "s/APPPORT/$APPPORT/g" ./deploy/Dockerfile -i

docker build -f ./deploy/Dockerfile -t $IMAGE_NAME .
docker images | grep $APP_NAME

# push
docker push $IMAGE_NAME
curl $REGISTRY/v2/_catalog

echo "$IMAGE_NAME Created and Publish"

echo "SET IMAGE_NAME -> $IMAGE_NAME"
sed "s/MY_REGISTRY/$REGISTRY/g" ./deploy/deployment.yaml -i
sed "s/IMAGE_ID/$IMAGE_ID/g" ./deploy/deployment.yaml -i
sed "s/VERSION/$IMAGE_VERSION/g" ./deploy/deployment.yaml -i
sed "s/CONNECTION_STRING_VALUE/$CONNECTION_STRING/g" ./deploy/deployment.yaml -i

sed "s/AppName/$APP_NAME/g" ./deploy/deployment.yaml -i
sed "s/AppName/$APP_NAME/g" ./deploy/service.yaml -i
sed "s/AppName/$APP_NAME/g" ./deploy/ingress.yaml -i

echo "SET NAMESPACE -> " $NAMESPACE
sed "s/NAMESPACE/$NAMESPACE/g" ./deploy/deployment.yaml -i
sed "s/NAMESPACE/$NAMESPACE/g" ./deploy/service.yaml -i
sed "s/NAMESPACE/$NAMESPACE/g" ./deploy/ingress.yaml -i

echo "SET APPPORT -> " $APPPORT
sed "s/APPPORT/$APPPORT/g" ./deploy/deployment.yaml -i
sed "s/APPPORT/$APPPORT/g" ./deploy/service.yaml -i
sed "s/APPPORT/$APPPORT/g" ./deploy/ingress.yaml -i

echo "SET URL_PREFIX -> " $URL_PREFIX
sed "s/URL_PREFIX/$URL_PREFIX/g" ./deploy/ingress.yaml -i


kubectl get namespace -o=custom-columns=NAME:.metadata.name  | grep ^${NAMESPACE}$ || ( echo "Creating Namespace '$NAMESPACE'" &&  kubectl create namespace $NAMESPACE )

# kubectl create namespace $NAMESPACE

# kubectl apply -f ./deploy/deployment.yaml
# kubectl apply -f ./deploy/service.yaml
# kubectl apply -f ./deploy/ingress.yaml

# kubectl -n $NAMESPACE get pods
# kubectl -n $NAMESPACE get deploy
# kubectl -n $NAMESPACE get services
# kubectl -n $NAMESPACE get ingress
