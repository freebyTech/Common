@Library('freebyTech')_

import com.freebyTech.BuildInfo
import com.freebyTech.NugetPushOptionEnum

String versionPrefix = '1.0'
String repository = 'freebytech'    
String imageName = 'common'
String dockerBuildArguments = ''

node 
{
  build(this, versionPrefix, repository, imageName, dockerBuildArguments, true, false, NugetPushOptionEnum.PushDebug, 'freebyTech.Common')
}

