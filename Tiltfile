load('ext://namespace', 'namespace_create', 'namespace_inject')

allow_k8s_contexts('summershop')
namespace_create('summershop')

k8s_yaml(namespace_inject(read_file('k8s/postgres/configmap.yaml'), 'summershop'))
k8s_yaml(namespace_inject(read_file('k8s/postgres/persistentvolume.yaml'), 'summershop'))
k8s_yaml(namespace_inject(read_file('k8s/postgres/persistentvolumeclaim.yaml'), 'summershop'))
k8s_yaml(namespace_inject(read_file('k8s/postgres/statefulset.yaml'), 'summershop'))
k8s_yaml(namespace_inject(read_file('k8s/postgres/service.yaml'), 'summershop'))

k8s_resource('postgres', port_forwards=[5432])

if config.tilt_subcommand == "down":
    local('kubectl delete all --all -n summershop', echo_off=True, quiet=True)